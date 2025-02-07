using Model.Models;
using Repository.Interfaces;
using Services.Interfaces;


namespace Services
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepo _accountRepository;

        public AccountService(IAccountRepo userRepository)
        {
            _accountRepository = userRepository;
        }

        public Task<Account?> AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> LoginAsync(string email, string password)
        {
            var user = await _accountRepository.GetAccountByEmail(email);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> RegisterAccountAsync(string email, string password, string name, string phoneNumber)
        {
            if (await _accountRepository.GetAccountByEmail(email) != null)
                return false; // Email đã tồn tại

            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var account = new Account
            {
                Email = email,
                Password = password,
                Name = name,
                PhoneNumber = phoneNumber,
                Role = "user"
            };
            await _accountRepository.RegisterAccount(account);
            return true;
        }

       
    }
}
