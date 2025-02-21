using Repositories.UnitOfWork;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CartService : ICartService
    {
        public readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddToCart(CartDTO cartDto)
        {
            if (cartDto.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            var cartRepository = _unitOfWork.GetRepository<Cart>();

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var existingCartItem = await cartRepository.FindAsync(c => c.UserId == cartDto.UserId &&
                                                                      c.BlindBoxId == cartDto.BlindBoxId &&
                                                                      c.PackageId == cartDto.PackageId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartDto.Quantity;
                await cartRepository.UpdateAsync(existingCartItem);
            }
            else
            {
                var cartItem = new Cart
                {
                    UserId = cartDto.UserId,
                    BlindBoxId = cartDto.BlindBoxId,
                    PackageId = cartDto.PackageId,
                    Quantity = cartDto.Quantity,
                    CreateDate = DateTime.UtcNow
                };
                await cartRepository.InsertAsync(cartItem);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Cart>> GetCartByUserId(string userId)
        {
            var cartRepository = _unitOfWork.GetRepository<Cart>();
            return await cartRepository.FindListAsync(c => c.UserId == userId);
        }
    }
}

