using Business.Users;
using DataAccess.Models;

namespace Business.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address? Address { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public UserModelDto? Buyer { get; set; }
        public UserModelDto? Seller { get; set; }
        public List<OrderItemDto>? Items { get; set; }
        public bool IsDeleted { get; set; }
        public required string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public required DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }

    }
}
