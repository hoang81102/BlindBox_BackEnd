
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class BlindBoxDbContext: DbContext
    {
        public BlindBoxDbContext(DbContextOptions<BlindBoxDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<BlindBox> BlindBoxes { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartDetail> CartDetail { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<PackageImage> PackageImages { get; set; }

        public DbSet<Address> Address { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
