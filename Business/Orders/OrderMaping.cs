
using Business.Products;
using Business.Users;
using DataAccess.Entities;
using DataAccess.Models;

namespace Business.Orders
{
    public static class OrderMaping
    {
        public static OrderEntity? MapModelToEntity(this OrderModel orderModel,
            List<OrderItemEntity> items,  
            OrderStatus status, decimal totalAmount,
            double CommissionAmount)
        {
            if (orderModel == null) return null;
            var orderEntity = new OrderEntity
            {
                BuyerId = orderModel.BuyerId,
                SellerId = orderModel.SellerId,
                Address = orderModel.Address,
                Items = items,
                CommissionAmount = CommissionAmount,
                Status = status,
                TotalAmount = totalAmount,
                CreatedBy = "-",
                ModifiedBy = "-",
                CreatedOn = DateTimeOffset.Now,
            };
            return orderEntity;
        }

        public static OrderItemEntity? MapOrderItemModelToEntity(this OrderItemModel orderItemModel)
        {
            if (orderItemModel == null) return null;
            var orderItemEntity = new OrderItemEntity
            {
                ProductId = orderItemModel.ProductId,
                Quantity = orderItemModel.Quantity,
                CreatedBy = "-",
                ModifiedBy = "-",
                CreatedOn = DateTimeOffset.Now,
            };
            return orderItemEntity;
        }

        public static OrderDto MapEntityToDto(this OrderEntity orderEntity)
        {
            if (orderEntity == null) return null;

            return new OrderDto
            {
                Id = orderEntity.Id,
                OrderDate = orderEntity.OrderDate,
                TotalAmount = orderEntity.TotalAmount,
                Status = orderEntity.Status.ToString(),
                Address = orderEntity.Address != null
                    ? new Address
                    {
                        Street = orderEntity.Address.Street,
                        City = orderEntity.Address.City,
                        State = orderEntity.Address.State,
                        PostalCode = orderEntity.Address.PostalCode
                    }
                    : null,

                Buyer = orderEntity.Buyer != null
                    ? new UserModelDto
                    {
                        Id = orderEntity.Buyer.Id,
                        Name = orderEntity.Buyer.Name
                    }
                    : null,

                Seller = orderEntity.Seller != null
                    ? new UserModelDto
                    {
                        Id = orderEntity.Seller.Id,
                        Name = orderEntity.Seller.Name
                    }
                    : null,

                Items = orderEntity.Items?.Select(i => new OrderItemDto
                {
                    Product = new ProductModelDto { Id = i.ProductId, Name = i.Product!.Name, Price = i.Product!.Price },
                    Quantity = i.Quantity
                }).ToList(),
                CreatedBy = orderEntity.CreatedBy,
                CreatedOn = orderEntity.CreatedOn,
                IsDeleted = orderEntity.IsDeleted,  
                ModifiedBy = orderEntity.ModifiedBy,
                ModifiedOn = orderEntity.ModifiedOn
            };
        }

    }

}
