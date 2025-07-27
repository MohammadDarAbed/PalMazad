using Business.Carts;
using Business.CartShopping;
using Business.Products;
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
            var cartItems = await _cartRepository.GetCartItemsAsync(cartId);
            if (cartItems == null || cartItems.Count == 0)
                return new List<CartItemModelBo>();

            return cartItems.Select(ci => new CartItemModelBo
            {
                CartId = ci.CartId,
                Quantity = ci.Quantity,
                ProductId = ci.Product.Id,
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
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
            //await CheckIfNotExists(userId);
            return entity.MapEntityToBo();
    }

        // Helpers:

        //private async Task CheckIfNotExists(int id)
        //{
        //    var isExist = await _cartRepository.IsExistAsync(f => f.Id == id);
        //    if (!isExist)
        //    {
        //        // if record delete
        //        ExceptionManager.ThrowItemNotFoundException("Cart", id);
        //    }
        //}
    }
}
