
namespace DataAccess.Models
{
    public class CategoryModel
    {
        public string Name { get; set; } = default!;
        public bool IsDeleted { get; set; }
        public required string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public required DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }

    }
}
