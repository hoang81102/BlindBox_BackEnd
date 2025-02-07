using Model.Models;
using Repository.Interfaces;
using Services.Interfaces;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Services
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepo _accountRepository;
        private readonly EmailService _emailService;

        public AccountService(IAccountRepo userRepository, EmailService emailService)
        {
            _accountRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<Account> GetUserByEmailAsync(string email)
        {

            var user = await _accountRepository.GetAccountByEmail(email);
            return user;
        }





        public async Task<Account> LoginAsync(string email, string password)
        {
            var user = await _accountRepository.GetAccountByEmail(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException("Sai mật khẩu hoặc tài khoản");
            }

            if (!user.IsVerify)
            {
                throw new UnauthorizedAccessException("Tài khoản chưa được xác thực. Vui lòng kiểm tra email để xác thực tài khoản.");
            }

            return user; // Đăng nhập thành công
        }



        public async Task<bool> RegisterAccountAsync(string email, string password, string name, string phoneNumber)
        {
            if (await _accountRepository.GetAccountByEmail(email) != null)
                return false; 

           
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var account = new Account
            {
                Email = email,
                Password = hashedPassword,
                Name = name,
                PhoneNumber = phoneNumber,
                Role = "user",
                IsVerify = false // 🚨 Mặc định chưa xác thực
            };

            await _accountRepository.RegisterAccount(account);

            
            string token = _emailService.GenerateEmailVerificationToken(email);
               Console.WriteLine(token);
            await _emailService.SendVerificationEmail(email, token);

            return true;
        }
        

        public async Task VerifyAccountAsync(string email)
        {
            // Lấy thông tin tài khoản từ database
            var user = await _accountRepository.GetAccountByEmail(email);

            if (user == null)
            {
                throw new KeyNotFoundException("Tài khoản không tồn tại.");
            }

            // Kiểm tra nếu tài khoản đã được xác thực
            if (user.IsVerify)
            {
                throw new InvalidOperationException("Tài khoản đã được xác thực.");
            }

            // Cập nhật trạng thái xác thực
            user.IsVerify = true;

            // Lưu thay đổi vào database
            await _accountRepository.UpdateAsync(user);
            await _accountRepository.SaveChangesAsync();
        }

    }
}
