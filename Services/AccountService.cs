using Model.Models;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepo _userRepository;

        public AccountService(IAccountRepo userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Account> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAccountByEmail(email);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }

        public async Task RegisterAsync(Account user)
        {
            var existingUser = await _userRepository.GetAccountByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already registered.");
            }
            await _userRepository.RegisterAccount(user);
        }

        public async Task<Account?> AuthenticateAsync(string username, string password)
        {
            // Tìm kiếm người dùng trong cơ sở dữ liệu dựa trên tên đăng nhập và mật khẩu
            var user = await _userRepository.GetUserByUsernameAndPasswordAsync(username, password);
            return user;
        }
    }
}
