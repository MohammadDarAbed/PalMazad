

namespace Business.Categories
{
    public class CategoryModelBo
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsDeleted { get; set; }
        public required string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public required DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
