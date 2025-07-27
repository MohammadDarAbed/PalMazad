using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } // the user who owns the cart
        public UserEntity User { get; set; } = default!;

        public ICollection<CartItemEntity> Items { get; set; } = new List<CartItemEntity>();

    }
}
