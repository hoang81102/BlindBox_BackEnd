using Model.Models;
using Repository.Interfaces.PackageRepo;
using Services.Interfaces.PackageS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepo _packageRepo;

        public PackageService(IPackageRepo packageRepo)
        {
            _packageRepo = packageRepo;
        }

        Package IPackageService.CreatePackage(Package package)
        {
            return _packageRepo.CreatePackage(package);
        }

        List<Package> IPackageService.GetAllPackages()
        {
            return _packageRepo.GetAllPackages();
        }

        Package IPackageService.GetPackageById(int Id)
        {
            return _packageRepo.GetPackageById(Id);
        }

        void IPackageService.RemovePackage(Package package)
        {
            _packageRepo.RemovePackage(package);
        }

        Package IPackageService.UpdatePackage(Package package)
        {
            return _packageRepo.UpdatePackage(package);
        }
    }
}
