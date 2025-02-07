using Model.Models;

namespace Services.PackageSV
{
    public interface IPackageService
    {
        Package CreatePackage(Package package);
        void RemovePackage(Package package);
        List<Package> GetAllPackages();
        Package GetPackageById(int Id);
        Package UpdatePackage(Package package);
    }
}
