
namespace DataAccess.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductModel
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Category? CategoryId { get; set; }

        [StringLength(50, ErrorMessage = "Product QR code cannot exceed 50 characters.")]
        public string ProductQR { get; set; }
    }

}
