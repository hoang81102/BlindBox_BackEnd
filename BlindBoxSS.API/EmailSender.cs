using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // You can integrate a real email service here (e.g., SMTP, SendGrid)
        Console.WriteLine($"Sending email to {email}: {subject}");
        return Task.CompletedTask;
    }
}
