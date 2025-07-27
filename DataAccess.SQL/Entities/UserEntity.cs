using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = default!; // The user's full name
        public string Email { get; set; } = default!; // User's email for login and communication
        public string PhoneNumber { get; set; } = default!; // User's contact number
        public string PasswordHash { get; set; } = default!; // Encrypted password

        public bool IsSeller { get; set; } // Indicates whether the user is a seller or just a buyer
        public bool IsVerifiedSeller { get; set; } // Indicates whether the seller's identity is verified
        public bool IsIdentityHidden { get; set; } // Hides seller info from buyer to prevent bypassing the platform

        // Navigation Properties
        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>(); // Products listed by this user
        public ICollection<OrderEntity> OrdersAsBuyer { get; set; } = new List<OrderEntity>(); // Orders made by this user
        public ICollection<OrderEntity> OrdersAsSeller { get; set; } = new List<OrderEntity>(); // Orders where this user is the seller
        public CartEntity? Cart { get; set; } // optional one-to-one cart, full navigation from User to their cart

    }


}
