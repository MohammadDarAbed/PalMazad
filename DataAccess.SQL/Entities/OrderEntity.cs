using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class OrderEntity : BaseEntity
    {
        public int BuyerId { get; set; }               // The buyer (customer) who placed the order
        public UserEntity Buyer { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal TotalAmount { get; set; }
        public double CommissionAmount { get; set; } // Platform's earned commission
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address? Address { get; set; }
        public List<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();
        public PaymentEntity? Payment { get; set; } // Payment related to this order

    }

}
