using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AddCartItemsRequest
    {
        public List<CartItemModel> Items { get; set; } = new();
    }
}
