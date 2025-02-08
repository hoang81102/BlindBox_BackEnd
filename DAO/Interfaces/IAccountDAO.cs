using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IAccountDAO
    {
        Task<Account> GetAccountByEmailAsync(string email);
        Task AddAccountAsync(Account user);
        Task<Account?> GetUserByUsernameAndPasswordAsync(string username, string password);

        Task<Account> GetAccountByAccountIdAsync(int accountId);
        Task UpdateAsync(Account account);
        Task SaveChangesAsync();

        Task UpdatePasswordAsync(Account account,string newpasswordhashed);
    }
}
