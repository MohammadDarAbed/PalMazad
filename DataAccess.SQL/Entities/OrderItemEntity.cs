

namespace DataAccess.Entities
{
    public class OrderItemEntity : BaseEntity
    {
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; } = default!;
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
