namespace DataAccess.Models
{
    public enum OrderStatus
    {
        Pending = 1,   // Order placed but not paid yet
        Paid = 2,      // Payment completed
        Delivered = 3, // Product delivered or service completed
        Cancelled = 4  // Order cancelled
    }
}
