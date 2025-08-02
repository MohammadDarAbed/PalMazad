
using Business.Products;

namespace Business.CartShopping
{
    public class CartItemModelBo
    {
        public int CartId {  get; set; }
        public ProductModelDto Product { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
