
namespace DataAccess.Models
{
    public class CategoryModel
    {
        public string Name { get; set; } = default!;
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }

    }
}
