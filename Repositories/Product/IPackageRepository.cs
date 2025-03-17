using Models;

namespace Repositories.Product
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllPackagesAsync();
        Task<Package?> GetPackageByIdAsync(Guid id);
        Task<Package> AddPackageAsync(Package package);
        Task<Package?> UpdatePackageAsync(Package package);
        Task<bool> DeletePackageAsync(Guid id);
        Task<Package?> GetPackageByImageIdAsync(Guid packageImageId);
    }
}
