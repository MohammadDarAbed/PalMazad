
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderEntity>> GetAllOrdersAsync();
        Task<OrderEntity?> GetByIdAsync(int id);
        Task CreateOrderAsync(OrderEntity entity);
        Task<OrderEntity> UpdateOrderAsync(OrderEntity entity);
        Task DeleteOrderAsync(int id);
        Task<bool> IsExistAsync(Expression<Func<OrderEntity, bool>> filters);

    }
    public class OrderRepository : IOrderRepository
    {
        private readonly IRepository<OrderEntity> _orderRepo;
        public OrderRepository(IRepository<OrderEntity> repo)
        {
            _orderRepo = repo;
        }

        public async Task<List<OrderEntity>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetItemsAsync(
                filter: p => p.IsDeleted == false,
                includes: query => query
                .Include(p => p.Buyer)
                .Include(p => p.Items)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(su => su.Seller)
                 );
            return orders;
        }

        public async Task<OrderEntity?> GetByIdAsync(int id)
        {
            return await _orderRepo.GetByIdAsync(id,
                includes: query => query
                .Include(p => p.Buyer)
                .Include(p => p.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(su => su.Seller)
                 );
        }

        public async Task CreateOrderAsync(OrderEntity entity)
        {
            await _orderRepo.CreateAsync(entity);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepo.DeleteAsync(id);
        }

        public async Task<OrderEntity> UpdateOrderAsync(OrderEntity entity)
        {
            return await _orderRepo.UpdateAsync(entity);
        }

        public async Task<bool> IsExistAsync(Expression<Func<OrderEntity, bool>> filters)
        {
            return await _orderRepo.ExistsAsync(new[] { filters });
        }
    }
}
