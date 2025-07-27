using Business.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CartShopping
{
    public class CartItemModelBo
    {
        public int CartId {  get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
