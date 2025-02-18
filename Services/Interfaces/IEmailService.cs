using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task SendVerificationEmail(string toEmail, string token);
        Task SendResetPasswordEmail(string toEmail, string token);
        string GenerateEmailVerificationToken(string email);
        string GeneratePasswordResetToken(string email);
    }

}
