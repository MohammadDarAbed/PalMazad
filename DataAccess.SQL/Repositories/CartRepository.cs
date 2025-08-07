using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItemEntity>> GetCartItemsAsync(int cartId);
        Task<bool> AddItemsToCartAsync(int userId, List<CartItemEntity> items);
        Task<CartEntity?> GetCartByUserIdAsync(int userId);
        Task<bool> IsExistAsync(Expression<Func<CartEntity, bool>> filters);
    }

    public class CartRepository : ICartRepository
    {
        private readonly IRepository<CartItemEntity> _cartItemRepo;
        private readonly IRepository<CartEntity> _cartRepo;

        public CartRepository(IRepository<CartItemEntity> cartItemRepo, IRepository<CartEntity> cartRepo)
        {
            _cartItemRepo = cartItemRepo;
            _cartRepo = cartRepo;
        }

        public async Task<List<CartItemEntity>> GetCartItemsAsync(int cartId)
        {
            // Get all cart items filtered by cartId and include Product navigation property
            return await _cartItemRepo.GetItemsAsync(
                filter: ci => ci.CartId == cartId,
                includes: query => query
                .Include(ci => ci.Product)
                    .ThenInclude(ps => ps.Seller)
                );
        }

        public async Task<bool> AddItemsToCartAsync(int userId, List<CartItemEntity> items)
        {
            // Find or create cart for user
            var carts = await _cartRepo.GetItemsAsync(c => c.UserId == userId, includes: query => query.Include(c => c.Items));
            var cart = carts.FirstOrDefault();

            if (cart == null)
            {
                cart = new CartEntity { UserId = userId, Items = new List<CartItemEntity>(), CreatedBy = "", CreatedOn = DateTimeOffset.Now };
                await _cartRepo.CreateAsync(cart);
            }

            foreach (var newItem in items)
            {
                // Check if item already exists in cart
                var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == newItem.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += newItem.Quantity;
                    await _cartItemRepo.UpdateAsync(existingItem);
                }
                else
                {
                    newItem.CartId = cart.Id;
                    await _cartItemRepo.CreateAsync(newItem);
                }
            }

            // Save changes
            await _cartRepo.SaveChangesAsync(cart);
            return true;
        }

        public async Task<CartEntity?> GetCartByUserIdAsync(int userId)
        {
            var carts = await _cartRepo.GetItemsAsync(
                filter: c => c.UserId == userId && !c.IsDeleted,
                includes: query => query
                    .Include(c => c.Items)
                        .ThenInclude(ci => ci.Product)
            );

            return carts.FirstOrDefault();
        }

        public async Task<bool> IsExistAsync(Expression<Func<CartEntity, bool>> filters)
        {
            return await _cartRepo.ExistsAsync(new[] { filters });
        }
    }
}
