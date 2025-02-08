using DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.DAO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AccountDAO : IAccountDAO
    {
        private readonly LAPTOPTHINHContext _context;

        public AccountDAO(LAPTOPTHINHContext context)
        {
            _context = context;
        }


        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAccountAsync(Account user)
        {
            await _context.Accounts.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Account?> GetUserByUsernameAndPasswordAsync(string email, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password );  // && u.IsDeleted == false
        }

        public async Task<Account> GetAccountByAccountIdAsync(int accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(u => u.AccountId == accountId);
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(Account account, string newpasswordhashed)
        {
            account.Password = newpasswordhashed;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
