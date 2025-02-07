using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Microsoft.EntityFrameworkCore;
using Services.BlindBoxSV;
using System.Data;

namespace BlindBoxSS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlindBoxController : Controller
    {
        private readonly IBlindBoxService _blindBoxService;

        public BlindBoxController(IBlindBoxService blindBoxService)
        {
            _blindBoxService = blindBoxService;
        }

        [HttpGet("GetAllBlindBox")]
        public IActionResult GetAllBlindBox()
        {
            return Ok(_blindBoxService.GetAllBlindBoxs());
        }

        [HttpGet("{id}")]
        public IActionResult GetBlindBoxById(int id)
        {
            var blindBox = _blindBoxService.GetBlindBoxById(id);
            if (blindBox == null)
            {
                return NotFound();
            }
            return Ok(blindBox);
        }

        [HttpPost("CreateBlindBox")]
        public IActionResult CreateBlindBox([FromBody] BlindBox blindBox)
        {
            var blindBoxCreated = new BlindBox
            {
                BlindBoxName = blindBox.BlindBoxName,
                PackageId = blindBox.PackageId,
                Price = blindBox.Price,
                Description = blindBox.Description,
                Stock = blindBox.Stock,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Percent = blindBox.Percent,
                BlindBoxStatus = blindBox.BlindBoxStatus,
                BlindBoxImages = blindBox.BlindBoxImages,
                CartDetails = blindBox.CartDetails,
                OrderDetails = blindBox.OrderDetails,
                Package = blindBox.Package,
            };

            _blindBoxService.CreatePackage(blindBoxCreated);
            return Ok(blindBoxCreated);
        }

        [HttpPut("UpdateBlindBox")]
        public IActionResult UpdateBlindBox(int id, [FromBody] BlindBox blindBox)
        {
            var blindBoxUpdated = new BlindBox
            {
                BlindBoxId = id,
                BlindBoxName = blindBox.BlindBoxName,
                PackageId = blindBox.PackageId,
                Price = blindBox.Price,
                Description = blindBox.Description,
                Stock = blindBox.Stock,
                CreatedAt = blindBox.CreatedAt,
                UpdatedAt = DateTime.Now,
                Percent = blindBox.Percent,
                BlindBoxStatus = blindBox.BlindBoxStatus,
                BlindBoxImages = blindBox.BlindBoxImages,
                CartDetails = blindBox.CartDetails,
                OrderDetails = blindBox.OrderDetails,
                Package = blindBox.Package,
            };
            try
            {
                _blindBoxService.UpdateBlindBox(blindBoxUpdated);
            }
            catch (DBConcurrencyException)
            {
                if (_blindBoxService.GetBlindBoxById(id) == null)
                {
                    return NotFound();
                }
                else
                { throw; }
            }
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlindBox(int id)
        {
            var blindBox = _blindBoxService.GetBlindBoxById(id);
            if (blindBox == null)
            {
                return NotFound();
            }
            _blindBoxService.RemovePackage(blindBox);
            return Ok(id);
        }
    }
}
