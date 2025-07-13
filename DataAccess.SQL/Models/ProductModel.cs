
namespace DataAccess.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Category CategoryId { get; set; }
        public string ProductQR { get; set; }
    }
}
