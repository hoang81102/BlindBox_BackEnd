using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IWalletDao
    {
        Task<List<Wallet>> GetAllAsync();
        Task<Wallet> GetByIdAsync(int walletId);
        Task CreateAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
        Task DeleteAsync(int walletId);
         Task<Wallet> GetByAccountIdAsync(int accountId);
    }
}
