using DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepo: IAccountRepo
    {
        private readonly IAccountDAO _accountDAO;

        public AccountRepo(IAccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        public  async Task<Account> GetAccountByEmail(string email)
        {
            return  await _accountDAO.GetAccountByEmailAsync(email);
        }

        public async Task<Account?> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
          return await _accountDAO.GetUserByUsernameAndPasswordAsync(username, password);
        }

        public async Task RegisterAccount(Account account)
        {
            await _accountDAO.AddAccountAsync(account);
        }

       

    }
}
