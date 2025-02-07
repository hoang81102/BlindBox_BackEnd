using DAO;
using Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.PackageRepo
{
    public class PackageRepo : IPackageRepo
    {
        private readonly BlindBoxDBContext _context;

        public PackageRepo(BlindBoxDBContext context)
        {
            _context = context;
        }

        public Package CreatePackage(Package package)
        {
            _context.Packages.Add(package);
            _context.SaveChanges();
            return package;
        }

        public void RemovePackage(Package package)
        {
            _context.Packages.Remove(package);
            _context.SaveChanges();
        }

        public List<Package> GetAllPackages()
        {
            return _context.Packages.ToList();
        }

        public Package GetPackageById(int Id)
        {
            return _context.Packages.FirstOrDefault(p => p.PackageId == Id);
        }

        public Package UpdatePackage(Package package)
        {
            _context.Entry(package).State = EntityState.Modified;
            _context.SaveChanges();
            return package;
        }
    }
}
