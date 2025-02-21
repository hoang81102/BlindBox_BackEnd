using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;

namespace BlindBoxSS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] CartDTO cartDto)
        {
            if (cartDto == null)
                return BadRequest("Invalid cart data");

            try
            {
                await _cartService.AddToCart(cartDto);
                return Ok(new { Message = "Item added to cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("get-cart/{userId}")]
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            try
            {
                var cartItems = await _cartService.GetCartByUserId(userId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
   }
