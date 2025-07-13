
namespace DataAccess.Entities;

public class OrderEntity : BaseEntity
{
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; }
    public string BuyerName { get; set; }
    public string BuyerPhone { get; set; }
    public bool IsDelivered { get; set; } = false;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

}


