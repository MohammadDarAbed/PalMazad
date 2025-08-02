namespace DataAccess.Models
{
    public enum OrderStatus
    {
        Pending = 1,   // Order placed but not paid yet
        Processing = 2,     // Being prepared
        Paid = 3,      // Payment completed
        Shipped = 4,        // Shipped to customer
        Delivered = 5, // Product delivered or service completed
        Cancelled = 6  // Order cancelled
    }
}
