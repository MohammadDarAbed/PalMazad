
using DataAccess.Entities;

namespace DataAccess.Models
{
    public class OrderModel
    {
        public int BuyerId { get; set; }
        public Address? Address { get; set; }
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
        public string? Notes { get; set; } // Optional: Any extra info from buyer
    }
}
