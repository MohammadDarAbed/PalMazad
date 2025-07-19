
using DataAccess.Models;

namespace DataAccess.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; } = default!; // Product name/title
    public string? Description { get; set; } = default!; // Detailed product or service description
    public decimal Price { get; set; } // Price of the product or service
    public string ProductQR { get; set; }

    public ProductCondition Condition { get; set; } // New, Used, or Service

    public bool IsPublished { get; set; } = true; // Controls whether the product is visible to buyers
    public bool IsHiddenSellerInfo { get; set; } // If true, the buyer can't see seller details

    public int SellerId { get; set; } // FK to User (seller)
    public UserEntity Seller { get; set; } = default!; // Navigation to seller

    public int CategoryId { get; set; } // FK to category
    public CategoryEntity Category { get; set; } = default!; // Navigation to product category

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>(); // Orders related to this product
}

