namespace DataAccess.Models
{
    public enum OrderStatus
    {
        Pending = 1,   // Order placed but not paid yet
        Processing = 2,     // Being prepared
        Shipped = 3,        // Shipped to customer
        Delivered = 4, // Product delivered or service completed
        Cancelled = 5  // Order cancelled
    }

    public enum PaymentStatus
    {
        Unpaid = 1,
        Paid = 2,
        Failed = 3,
        Refunded = 4
    }
}
