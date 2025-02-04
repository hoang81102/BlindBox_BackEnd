using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account?> AuthenticateAsync(string username, string password);
        Task<Account> LoginAsync(string email, string password);
        Task<bool> RegisterAccountAsync(string email, string password, string name, string phoneNumber);
    }
}
