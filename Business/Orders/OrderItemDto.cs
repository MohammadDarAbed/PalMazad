
using Business.Products;

namespace Business.Orders
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        public ProductModelDto Product { get; set; }
    }

}
