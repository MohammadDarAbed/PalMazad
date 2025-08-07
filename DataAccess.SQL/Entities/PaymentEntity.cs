
namespace DataAccess.Entities
{
    public class PaymentEntity : BaseEntity
    {
        public int OrderId { get; set; } // FK to order
        public OrderEntity Order { get; set; } = default!; // Related order
        public decimal Amount { get; set; } // Total payment amount
        public string PaymentMethod { get; set; } = default!; // e.g., Credit Card, PayPal, etc.
        public string? TransactionId { get; set; } // Gateway transaction ID
    }

}
