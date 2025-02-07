using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendVerificationEmail(string toEmail, string token)
    {
        Console.WriteLine($"🔹 Email sẽ gửi tới: {toEmail}");
        Console.WriteLine($"🔹 Token gửi đi: {token}");

        // Kiểm tra nếu token bị null hoặc rỗng
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("⚠️ Token bị null hoặc rỗng!");
            return;
        }

        var encodedEmail = HttpUtility.UrlEncode(toEmail);
        var encodedToken = HttpUtility.UrlEncode(token);
        string verificationUrl = $"https://yourfrontend.com/verify?email={encodedEmail}&token={encodedToken}";

        string emailBody = $@"
        <html>
        <body>
            <p>Chào bạn,</p>
            <p>Vui lòng <a href='{verificationUrl}'>Tại đây</a> để xác thực tài khoản.</p>
            <p>Cảm ơn!</p>
        </body>
        </html>";

        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", _config["EmailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Xác thực tài khoản";
            var bodyBuilder = new BodyBuilder { HtmlBody = emailBody };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), false);
            await client.AuthenticateAsync(_config["EmailSettings:SenderEmail"], _config["EmailSettings:SenderPassword"]);

            Console.WriteLine("✅ Đã kết nối SMTP!");

            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            Console.WriteLine("✅ Email đã được gửi thành công!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Lỗi gửi email: {ex.Message}");
        }
    }




   public string GenerateEmailVerificationToken(string email)
{
    var secretKey = _config["Jwt:SecretKey"];
    if (string.IsNullOrEmpty(secretKey))
    {
        throw new Exception("⚠️ LỖI: Jwt:SecretKey không được null!");
    }

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
        Expires = DateTime.UtcNow.AddHours(24),
        Issuer = _config["Jwt:Issuer"],   // ✅ Thêm Issuer
        Audience = _config["Jwt:Audience"], // ✅ Thêm Audience
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}



}
