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
    public class WalletService: IWalletService
    {
        private readonly IWalletRepo _walletRepository;

        public WalletService(IWalletRepo walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public Task<List<Wallet>> GetAllWalletsAsync()
        {
            return _walletRepository.GetAllWalletsAsync();
        }

        public Task<Wallet> GetWalletByIdAsync(int walletId)
        {
            return _walletRepository.GetWalletByIdAsync(walletId);
        }

        public Task CreateWalletAsync(Wallet wallet)
        {
            return _walletRepository.CreateWalletAsync(wallet);
        }

        public Task UpdateWalletAsync(Wallet wallet)
        {
            return _walletRepository.UpdateWalletAsync(wallet);
        }

        public Task DeleteWalletAsync(int walletId)
        {
            return _walletRepository.DeleteWalletAsync(walletId);
        }

        public Task<Wallet> GetWalletByAccountIdAsync(int accountId)
        {
            return _walletRepository.GetWalletByAccountIdAsync(accountId);
        }
    }
}
