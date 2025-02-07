using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.PackageS;
using Model.Models;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet("GetAllPackage")]
        public IActionResult GetAllPackage()
        {
            return Ok(_packageService.GetAllPackages());
        }

        [HttpGet("{id}")]
        public IActionResult GetPackageById(int id)
        {
            var package = _packageService.GetPackageById(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }

        [HttpPost("CreatePackage")]
        public IActionResult CreatePackage([FromBody] Package package)
        {
            var packageCreated = new Package
            {

                CategoryId = package.CategoryId,
                Amount = package.Amount,
                PackageStatus = package.PackageStatus,
                BlindBoxes = package.BlindBoxes,
                CartDetails = package.CartDetails,
                Category = package.Category,
                OrderDetails = package.OrderDetails,
                PackageImages = package.PackageImages,
            };

            _packageService.CreatePackage(packageCreated);
            return Ok(packageCreated);
        }

        [HttpPut("UpdatePackage")]
        public IActionResult UpdatePackage(int id, [FromBody] Package package)
        {
            var packageUpdated = new Package
            {
                PackageId = id,
                CategoryId = package.CategoryId,
                Amount = package.Amount,
                PackageStatus = package.PackageStatus,
                BlindBoxes = package.BlindBoxes,
                CartDetails = package.CartDetails,
                Category = package.Category,
                OrderDetails = package.OrderDetails,
                PackageImages = package.PackageImages,
            };
            try
            {
                _packageService.UpdatePackage(packageUpdated);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_packageService.GetPackageById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public IActionResult RemovePackage(int id)
        {
            var package = _packageService.GetPackageById(id);
            if (package == null)
            {
                return NotFound();
            }
            _packageService.RemovePackage(package);
            return Ok();
        }


    }
}
