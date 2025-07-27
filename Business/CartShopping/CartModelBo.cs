using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CartShopping
{
    public class CartModelBo
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public List<CartItemModelBo> Items { get; set; } = new List<CartItemModelBo>();

        public bool IsDeleted { get; set; }
        public required string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public required DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? ModifiedOn { get; set; }

    }
}
