
namespace DataAccess.Models
{
    public class CartCheckoutModel
    {
        public int BuyerId { get; set; }
        public Address Address { get; set; } = default!; // Required for shipping/delivery
        public string? PaymentMethod { get; set; } // Optional: e.g., "CashOnDelivery", "CreditCard"
        public string? Notes { get; set; } // Optional: Any extra info from buyer
    }

}
