


namespace DataAccess.Entities
{
    public class CartEntity : BaseEntity
    {
        public int UserId { get; set; } // the user who owns the cart
        public UserEntity User { get; set; } = default!;

        public ICollection<CartItemEntity> Items { get; set; } = new List<CartItemEntity>();

    }
}
