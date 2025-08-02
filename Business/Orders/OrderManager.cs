
using Business.Shared;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Business.Orders
{
    public interface IOrderManager
    {
        Task<List<OrderDto>> GetOrders();
        Task<OrderDto> GetOrderById(int orderId);
        Task<OrderDto> CreateOrder(OrderModel orderModel);
        Task<OrderDto> UpdateOrder(int orderId, OrderModel orderModel);
        Task DeleteOrder(int id);
    }

    public class OrderManager(IOrderRepository _orderRepo, 
        IProductRepository _productRepo,
        IUserRepository _userRepo) : IOrderManager
    {
        public async Task<List<OrderDto>> GetOrders()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            return orders.Select(p => p.MapEntityToDto()).ToList();

        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            await CheckIfNotExists(orderId);
            var order = await _orderRepo.GetByIdAsync(orderId);
            Validations.CheckIfEntityDeleted(order.IsDeleted, orderId, "Order");
            return order.MapEntityToDto();
        }
        public async Task<OrderDto> CreateOrder(OrderModel orderModel)
        {
            await CheckIfBuyerExists(orderModel);
            var (items, totalAmount) = await GetItemsAndTotalAmount(orderModel);
            var entity = orderModel.MapModelToEntity(items, OrderStatus.Pending, totalAmount, 0.10);

            entity.BuyerId = orderModel.BuyerId;

            await _orderRepo.CreateOrderAsync(entity);

            var savedOrder = await _orderRepo.GetByIdAsync(entity.Id);
            return savedOrder.MapEntityToDto();
        }

        public async Task DeleteOrder(int id)
        {
            await _orderRepo.DeleteOrderAsync(id);
        }

        public async Task<OrderDto> UpdateOrder(int orderId, OrderModel orderModel)
        {
            var currentOrder = await _orderRepo.GetByIdAsync(orderId);
            if (currentOrder == null) ExceptionManager.ThrowItemNotFoundException("Order", orderId);
            await CheckIfBuyerExists(orderModel);

            var (items, totalAmount) = await GetItemsAndTotalAmount(orderModel);

            var entity = orderModel.MapModelToEntity(items, currentOrder!.Status, totalAmount, 0.10);
            entity.BuyerId = orderModel.BuyerId;

            entity!.Id = orderId;
            var updatedOrder = await _orderRepo.UpdateOrderAsync(entity);
            var savedUpdatedOrder = await _orderRepo.GetByIdAsync(entity.Id);
            return savedUpdatedOrder.MapEntityToDto();
        }

        // Helpers:

        private async Task CheckIfNotExists(int id)
        {
            var isExist = await _orderRepo.IsExistAsync(f => f.Id == id);
            if (!isExist)
            {
                ExceptionManager.ThrowItemNotFoundException("Order", id);
            }
        }

        private async Task<(List<OrderItemEntity> Items, decimal TotalAmount)> GetItemsAndTotalAmount(OrderModel orderModel)
        {
            List<OrderItemEntity> items = [];
            decimal totalAmount = 0;
            foreach (var item in orderModel.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null) ExceptionManager.ThrowItemNotFoundException("Product", item.ProductId);
                var orderItemEntity = new OrderItemEntity {
                    ProductId = product!.Id,
                    Quantity = item.Quantity,
                    CreatedBy = "", CreatedOn = DateTimeOffset.Now};
                items.Add(orderItemEntity);
                totalAmount += ( product.Price * item.Quantity);
            }
            return (items, totalAmount);
        }

        private async Task CheckIfBuyerExists(OrderModel orderModel)
        {
            var buyer = await _userRepo.GetByIdAsync(orderModel.BuyerId);
            if (buyer == null) ExceptionManager.ThrowItemNotFoundException("User(Buyer)", orderModel.BuyerId);
        }
    }
}
