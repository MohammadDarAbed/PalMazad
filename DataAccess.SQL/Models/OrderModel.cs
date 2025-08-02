
namespace DataAccess.Models
{
    public class OrderModel
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public Address? Address { get; set; }
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();

    }
}
