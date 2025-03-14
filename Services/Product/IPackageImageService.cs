using Models;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Product
{
     public interface IPackageImageService
    {
        Task AddPackageImage(PackageImageDTO packgeImageDTO);
        Task<IEnumerable<PackageImage>> GetPackageImages(Guid packageId);
        Task<bool> UpdatePackageImage(Guid packageimageId, string imageURL);
        Task<bool> DeletePackageImage(Guid packageimageId);
    }
}
