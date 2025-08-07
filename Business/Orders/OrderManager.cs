
using Business.CartShopping;
using Business.Managers;
using Business.Shared;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Observer.Publishers;

namespace Business.Orders
{
    public interface IOrderManager
    {
        Task<List<OrderDto>> GetOrders();
        Task<OrderDto> GetOrderById(int orderId);
        Task<OrderDto> CreateOrder(OrderModel orderModel);
        Task<OrderDto> CreateOrderFromCart(CartCheckoutModel orderModel, int cartId);
        Task<OrderDto> UpdateOrder(int orderId, OrderModel orderModel);
        Task<OrderDto> UpdatePaymentStatusAsync(int id, UpdatePaymentStatusModel model);
        Task DeleteOrder(int id);
    }

    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICartManager _cartManager;
        private readonly IOrderPublisher _orderPublisher;

        public OrderManager(
           IOrderRepository orderRepo,
           IProductRepository productRepo,
           IUserRepository userRepo,
           ICartManager cartManager,
           IOrderPublisher orderPublisher)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _userRepo = userRepo;
            _cartManager = cartManager;
            _orderPublisher = orderPublisher;
        }
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
            await CheckIfBuyerExists(orderModel.BuyerId);
            var (items, totalAmount) = await GetItemsAndTotalAmount(orderModel);
            var entity = orderModel.MapModelToEntity(items, OrderStatus.Pending, totalAmount, 0.10);

            entity.BuyerId = orderModel.BuyerId;

            await _orderRepo.CreateOrderAsync(entity);
            var savedOrder = await _orderRepo.GetByIdAsync(entity.Id);

            // Publish event after creation
            await _orderPublisher.PublishOrderCreated(savedOrder!.Id, totalAmount, savedOrder.Buyer.Email);
            return savedOrder.MapEntityToDto();
        }
        public async Task<OrderDto> CreateOrderFromCart(CartCheckoutModel orderModel, int cartId)
        {
            await CheckIfBuyerExists(orderModel.BuyerId);
            var cartItems = await _cartManager.GetCartItemsAsync(cartId);
            var totalAmount = CalculateCartItemsTotalAmount(cartItems);
            var orderItemsEntity = cartItems.Select(i => new OrderItemEntity
            {
                ProductId = i.Product.Id,
                Quantity = i.Quantity,
                CreatedBy = "",
                CreatedOn = DateTimeOffset.Now
            }).ToList();
            var entity = orderModel.MapModelToEntity(orderItemsEntity, OrderStatus.Pending, totalAmount, 0.10);
            entity.BuyerId = orderModel.BuyerId;
            await _orderRepo.CreateOrderAsync(entity);
            var savedOrder = await _orderRepo.GetByIdAsync(entity.Id);
            // Publish event after creation
            await _orderPublisher.PublishOrderCreated(savedOrder!.Id, totalAmount, savedOrder.Buyer.Email);
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
            await CheckIfBuyerExists(orderModel.BuyerId);

            var (items, totalAmount) = await GetItemsAndTotalAmount(orderModel);

            var entity = orderModel.MapModelToEntity(items, currentOrder!.Status, totalAmount, 0.10);
            entity.BuyerId = orderModel.BuyerId;

            entity!.Id = orderId;
            var updatedOrder = await _orderRepo.UpdateOrderAsync(entity);
            var savedUpdatedOrder = await _orderRepo.GetByIdAsync(entity.Id);
            return savedUpdatedOrder.MapEntityToDto();
        }

        public async Task<OrderDto> UpdatePaymentStatusAsync(int id, UpdatePaymentStatusModel model)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            await CheckIfNotExists(id);
            var items = await GetItemsntitiesForm(order!.Items);
            var entityUpdated = new OrderEntity
            {
                Id = id,
                BuyerId = order.BuyerId,
                Notes = order.Notes,
                TotalAmount = order.TotalAmount,
                Address = order.Address,
                PaymentStatus = model.Status,
                CreatedBy = order.CreatedBy,
                CreatedOn = order.CreatedOn,
                CommissionAmount = order.CommissionAmount,
                Status = order.Status,
                Payment = order.Payment,
                IsDeleted = order.IsDeleted,
                ModifiedBy = order.ModifiedBy,
                ModifiedOn = order.ModifiedOn,
                OrderDate = order.OrderDate,
                Items = items
            };
            var updatedOrder = await _orderRepo.UpdateOrderAsync(entityUpdated);
            var savedUpdatedOrder = await _orderRepo.GetByIdAsync(order.Id);
            return savedUpdatedOrder!.MapEntityToDto();
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
                var orderItemEntity = new OrderItemEntity
                {
                    ProductId = product!.Id,
                    Quantity = item.Quantity,
                    CreatedBy = "",
                    CreatedOn = DateTimeOffset.Now
                };
                items.Add(orderItemEntity);
                totalAmount += (product.Price * item.Quantity);
            }
            return (items, totalAmount);
        }

        private async Task<List<OrderItemEntity>> GetItemsntitiesForm(List<OrderItemEntity> items)
        {
            List<OrderItemEntity> orderItemEntities = [];
            foreach (var item in items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null) ExceptionManager.ThrowItemNotFoundException("Product", item.ProductId);
                var orderItemEntity = new OrderItemEntity
                {
                    ProductId = product!.Id,
                    Quantity = item.Quantity,
                    CreatedBy = "",
                    CreatedOn = DateTimeOffset.Now
                };
                orderItemEntities.Add(orderItemEntity);
            }
            return orderItemEntities;
        }

        private decimal CalculateCartItemsTotalAmount(List<CartItemModelBo> items)
        {
            decimal totalAmount = 0;
            foreach (var item in items)
            {
                totalAmount += (item.Product.Price * item.Quantity);
            }
            return totalAmount;
        }

        private async Task CheckIfBuyerExists(int buyerId)
        {
            var buyer = await _userRepo.GetByIdAsync(buyerId);
            if (buyer == null) ExceptionManager.ThrowItemNotFoundException("User(Buyer)", buyerId);
        }
    }
}
