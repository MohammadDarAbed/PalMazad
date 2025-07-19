using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; } = default!; // Category name (e.g., Electronics, Services)

        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>(); // Products under this category
    }
}
