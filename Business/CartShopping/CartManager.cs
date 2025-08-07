using Business.Carts;
using Business.CartShopping;
using Business.Products;
using Business.Users;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Business.Managers
{
    public interface ICartManager
    {
        Task<List<CartItemModelBo>> GetCartItemsAsync(int cartId);
        Task<bool> AddItemsToCartAsync(int userId, List<CartItemModel> items);
        Task<CartModelBo?> GetCartByUserIdAsync(int userId);
        // Add more method signatures here as needed, e.g. RemoveItem, ClearCart etc.
    }

    public class CartManager : ICartManager
    {
        private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<CartItemModelBo>> GetCartItemsAsync(int cartId)
        {
            await CheckIfNotExists(cartId);
            var cartItems = await _cartRepository.GetCartItemsAsync(cartId);
            if (cartItems == null || cartItems.Count == 0)
                return new List<CartItemModelBo>();

            return cartItems.Select(ci => new CartItemModelBo
            {
                CartId = ci.CartId,
                Product = new ProductModelDto
                {
                    Id = ci.Product.Id,
                    Name = ci.Product.Name,
                    Price = ci.Product.Price,
                    Seller = new UserModelDto
                    {
                        Id = ci.Product.Seller.Id,
                        Name = ci.Product.Seller.Name,
                        PhoneNumber = ci.Product.Seller.PhoneNumber,
                    },
                },
                Quantity = ci.Quantity,
                IsDeleted = ci.IsDeleted,

            }).ToList();
        }

        public async Task<bool> AddItemsToCartAsync(int userId, List<CartItemModel> items)
        {
            // TODO: Check if the items are exists
            var cartItems = items.Select(i => new CartItemEntity
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                CreatedOn = DateTimeOffset.Now,
                CreatedBy = "",
            }).ToList();

            return await _cartRepository.AddItemsToCartAsync(userId, cartItems);
        }

        public async Task<CartModelBo?> GetCartByUserIdAsync(int userId)
        {
            var entity = await _cartRepository.GetCartByUserIdAsync(userId);
            await CheckIfUserNotExists(userId);
            return entity.MapEntityToBo();
        }

        // Helpers:

        private async Task CheckIfNotExists(int id)
        {
            var isExist = await _cartRepository.IsExistAsync(f => f.Id == id);
            if (!isExist)
            {
                ExceptionManager.ThrowItemNotFoundException("Cart", id);
            }
        }

        private async Task CheckIfUserNotExists(int userId)
        {
            var isExist = await _cartRepository.IsExistAsync(f => f.UserId == userId);
            if (!isExist)
            {
                // if record delete
                ExceptionManager.ThrowItemNotFoundException("User", userId);
            }
        }
    }
}
