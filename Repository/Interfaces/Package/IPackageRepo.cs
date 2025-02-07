using Model.Models;

namespace Repository.Interfaces.PackageRepo
{
    public interface IPackageRepo
    {
        Package CreatePackage(Package package);
        void RemovePackage(Package package);
        List<Package> GetAllPackages();
        Package GetPackageById(int Id);
        Package UpdatePackage(Package package);
    }
}
