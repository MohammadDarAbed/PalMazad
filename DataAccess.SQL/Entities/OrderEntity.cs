
using DataAccess.Models;

namespace DataAccess.Entities;

public class OrderEntity : BaseEntity
{
    public int ProductId { get; set; } // FK to product
    public ProductEntity Product { get; set; } = default!; // Product that was ordered

    public int BuyerId { get; set; } // FK to user (buyer)
    public UserEntity Buyer { get; set; } = default!; // User who made the purchase
    public int SellerId { get; set; } // FK to user (seller)
    public UserEntity Seller { get; set; } = default!; // Seller of the product

    public decimal TotalAmount { get; set; } // Total amount paid by buyer
    public decimal CommissionAmount { get; set; } // Platform's earned commission

    public OrderStatus Status { get; set; } // Current status of the order

    public PaymentEntity? Payment { get; set; } // Payment related to this order
}
