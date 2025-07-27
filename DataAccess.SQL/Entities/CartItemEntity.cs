


using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public class CartItemEntity : BaseEntity
    {
        public int CartId { get; set; }

        [JsonIgnore] // Ignore the loop circle 
        public CartEntity Cart { get; set; } = default!;

        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = default!;

        public int Quantity { get; set; }
    }

}
