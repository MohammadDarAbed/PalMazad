
namespace Business.Users
{
    public class UserModelBo
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!; // The user's full name
        public string Email { get; set; } = default!; // User's email for login and communication
        public string PhoneNumber { get; set; } = default!; // User's contact number

        public bool IsSeller { get; set; } // Indicates whether the user is a seller or just a buyer
        public bool IsVerifiedSeller { get; set; } // Indicates whether the seller's identity is verified
        public bool IsIdentityHidden { get; set; } // Hides seller info from buyer to prevent bypassing the platform
        public bool IsDeleted { get; set; }
        public required string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public required DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? ModifiedOn { get; set; }


    }
}
