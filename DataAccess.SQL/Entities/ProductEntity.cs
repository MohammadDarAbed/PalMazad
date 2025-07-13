
using DataAccess.Models;

namespace DataAccess.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public Category CategoryId { get; set; }
    public required string ProductQR { get; set; }
}
