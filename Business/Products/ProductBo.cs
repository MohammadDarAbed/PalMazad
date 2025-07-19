
using Business.Users;
using DataAccess.Models;

namespace Business.Products
{
    public class ProductBo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public bool IsHiddenSellerInfo { get; set; }
        public required string ProductQR { get; set; }
        public UserModelBo Seller { get; set; }
        public bool IsDeleted { get; set; }
        public required string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public required DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }

    }
}
