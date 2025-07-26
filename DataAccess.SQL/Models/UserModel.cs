using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserModel
    {
        public string Name { get; set; } = default!; // The user's full name
        public string Email { get; set; } = default!; // User's email for login and communication
        public string PasswordHash { get; set; } = default!; // Encrypted password
        public string PhoneNumber { get; set; } = default!; // User's contact number
        public bool IsSeller { get; set; } // Indicates whether the user is a seller or just a buyer
        public bool IsVerifiedSeller { get; set; } // Indicates whether the seller's identity is verified
        public bool IsIdentityHidden { get; set; } // Hides seller info from buyer to prevent bypassing the platform
        public bool IsDeleted { get; set; }
    }
}
