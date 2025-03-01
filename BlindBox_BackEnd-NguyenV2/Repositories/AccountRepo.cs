using DAO;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepo : IAccountRepo
    {

        private readonly BlindBoxDbContext _context;

        public AccountRepo(BlindBoxDbContext context)
        {
            _context = context;
        }

       
    }
}
