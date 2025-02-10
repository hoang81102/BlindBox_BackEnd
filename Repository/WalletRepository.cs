using DAO.Interfaces;
using Model.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WalletRepository : IWalletRepo
    {
        private readonly IWalletDao _walletDao;

        public WalletRepository(IWalletDao walletDao)
        {
            _walletDao = walletDao;
        }

        public async Task<List<Wallet>> GetAllWalletsAsync()
        {
            return await _walletDao.GetAllAsync();
        }

        public async  Task<Wallet> GetWalletByIdAsync(int walletId)
        {
            return await _walletDao.GetByIdAsync(walletId);
        }

        public  Task CreateWalletAsync(Wallet wallet)
        {
            return  _walletDao.CreateAsync(wallet);
        }

        public  Task UpdateWalletAsync(Wallet wallet)
        {
            return  _walletDao.UpdateAsync(wallet);
        }

        public  Task DeleteWalletAsync(int walletId)
        {
            return  _walletDao.DeleteAsync(walletId);
        }

        public async Task<Wallet> GetWalletByAccountIdAsync(int accountId)
        {
            return await _walletDao.GetByAccountIdAsync(accountId);
        }
    }
}
