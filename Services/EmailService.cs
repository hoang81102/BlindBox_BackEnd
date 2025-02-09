using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Services.Interfaces;

public class EmailService:IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendVerificationEmail(string toEmail, string token)
    {
        var encodedEmail = HttpUtility.UrlEncode(toEmail);
        var encodedToken = HttpUtility.UrlEncode(token);
        string verificationUrl = $"http://localhost:5000/api/Auth?token={encodedToken}";
        string emailBody = $@"
<html>
<head>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            background-color: #f4f4f4;
            color: #333;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            max-width: 600px;
            background: #ffffff;
            border-radius: 10px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
            padding: 30px;
            margin: auto;
        }}
        .header {{
            text-align: center;
            background: linear-gradient(to right, #0078D7, #00ADEF);
            color: white;
            padding: 20px;
            border-radius: 10px 10px 0 0;
        }}
        .header h2 {{
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }}
        .logo {{
            display: block;
            margin: 20px auto;
            width: 120px;
            border-radius: 50%;
        }}
        .content {{
            text-align: center;
            padding: 20px;
            font-size: 16px;
            line-height: 1.6;
        }}
        .button {{
            background: linear-gradient(to right, #0078D7, #00ADEF);
            color: white;
            font-size: 16px;
            font-weight: bold;
            padding: 12px 24px;
            text-decoration: none;
            border-radius: 6px;
            display: inline-block;
            transition: 0.3s ease-in-out;
        }}
        .button:hover {{
            background: linear-gradient(to right, #005bb5, #008cba);
        }}
        .footer {{
            margin-top: 20px;
            font-size: 12px;
            text-align: center;
            color: #777;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h2>Account Verification</h2>
        </div>
        <img src='https://via.placeholder.com/120' alt='Logo' class='logo' />
        <div class='content'>
            <p>Welcome to ________!</p>
            <p>Please click the button below to verify your account:</p>
            <p>
                <a href='{verificationUrl}' class='button'>Verify Now</a>
            </p>
            <p>If you did not request this verification, please ignore this email.</p>
        </div>
        <div class='footer'>
            <p>Thank you!</p>
        </div>
    </div>
</body>
</html>";


        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Blind Box Sales Website", _config["EmailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Xác thực tài khoản";
            var bodyBuilder = new BodyBuilder { HtmlBody = emailBody };
            message.Body = bodyBuilder.ToMessageBody();
            using var client = new SmtpClient();
            await client.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), false);
            await client.AuthenticateAsync(_config["EmailSettings:SenderEmail"], _config["EmailSettings:SenderPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi gửi email: {ex.Message}");
        }
    }




   public string GenerateEmailVerificationToken(string email)
{
    var secretKey = _config["Jwt:SecretKey"];
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
        Expires = DateTime.UtcNow.AddMinutes(10),
        Issuer = _config["Jwt:Issuer"],   
        Audience = _config["Jwt:Audience"], 
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}

    public async Task SendResetPasswordEmail(string toEmail, string token)
    {
        var encodedToken = HttpUtility.UrlEncode(token);
        string resetPasswordUrl = $"http://localhost:5000/api/Auth/verify-reset-token?token={encodedToken}";
        string emailBody = $@"
<html>
<head>
    <style>
        body {{ font-family: 'Arial', sans-serif; background-color: #f4f4f4; color: #333; padding: 20px; }}
        .container {{ max-width: 600px; background: #ffffff; border-radius: 10px; padding: 30px; margin: auto; }}
        .header {{ text-align: center; background: linear-gradient(to right, #ff512f, #dd2476); color: white; padding: 20px; border-radius: 10px 10px 0 0; }}
        .header h2 {{ margin: 0; font-size: 24px; font-weight: bold; }}
        .content {{ text-align: center; padding: 20px; font-size: 16px; }}
        .button {{ background: linear-gradient(to right, #ff512f, #dd2476); color: white; padding: 12px 24px; text-decoration: none; border-radius: 6px; }}
        .footer {{ margin-top: 20px; font-size: 12px; text-align: center; color: #777; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'><h2>Reset Your Password</h2></div>
        <div class='content'>
            <p>Click the button below to reset your password:</p>
            <p><a href='{resetPasswordUrl}' class='button'>Reset Password</a></p>
            <p>If you did not request this, please ignore this email.</p>
        </div>
        <div class='footer'><p>Thank you!</p></div>
    </div>
</body>
</html>";

        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Blind Box Sales Website", _config["EmailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Reset Your Password";
            var bodyBuilder = new BodyBuilder { HtmlBody = emailBody };
            message.Body = bodyBuilder.ToMessageBody();
            using var client = new SmtpClient();
            await client.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), false);
            await client.AuthenticateAsync(_config["EmailSettings:SenderEmail"], _config["EmailSettings:SenderPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }


    public string GeneratePasswordResetToken(string email)
    {
        var secretKey = _config["Jwt:SecretKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }





}
