﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAccountRepo
    {
        Task<Account> GetAccountByEmail(string email);
        Task RegisterAccount(Account account);

        Task<Account?> GetUserByUsernameAndPasswordAsync(string username, string password);

        Task<Account> GetAccountByAccountId(int accountId);
        Task UpdateAsync(Account account);
        Task SaveChangesAsync();
        Task UpdatePasswordAsync(Account account, string newpasswordhashed);
    }
}
