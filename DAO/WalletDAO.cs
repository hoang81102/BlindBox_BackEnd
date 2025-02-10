using DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class WalletDAO : IWalletDao
    {
        private readonly BlindBoxDBContext _context;

        public WalletDAO(BlindBoxDBContext context)
        {
            _context = context;
        }

        public async Task<List<Wallet>> GetAllAsync()
        {
            return await _context.Set<Wallet>().ToListAsync();
        }

        public async Task<Wallet> GetByIdAsync(int walletId)
        {
            return await _context.Set<Wallet>().FindAsync(walletId);
        }

        public async Task CreateAsync(Wallet wallet)
        {
            _context.Set<Wallet>().Add(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _context.Set<Wallet>().Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int walletId)
        {
            var wallet = await GetByIdAsync(walletId);
            if (wallet != null)
            {
                _context.Set<Wallet>().Remove(wallet);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Wallet> GetByAccountIdAsync(int accountId)
        {
            var wallet = await _context.Wallets
                    .AsQueryable()
                    .Where(w => w.AccountId == accountId)
                    .FirstOrDefaultAsync();

            return wallet;
        }
    }
}
