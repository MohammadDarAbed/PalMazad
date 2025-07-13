
using DataAccess.Entities;
using DataAccess.Models;

namespace Business.Products
{
    public static class ProductMaping
    {
        public static ProductEntity? MapModelToEntity(this ProductModel productModel)
        {
            if (productModel == null) return null;
            var productEntity = new ProductEntity
            {
                Name = productModel.Name,
                CreatedBy = "-",
                Description = productModel.Description,
                CategoryId = productModel.CategoryId,
                Price = productModel.Price,
                ProductQR = productModel.ProductQR,
                ModifiedBy = "-",
                CreatedOn = DateTimeOffset.Now,
            };
            return productEntity;
        }

        public static ProductBo? MapEntityToBo(this ProductEntity productEntity)
        {
            if (productEntity == null) return null;
            var productBo = new ProductBo
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                CategoryId = productEntity.CategoryId,
                IsDeleted = productEntity.IsDeleted,
                Price = productEntity.Price,
                ProductQR = productEntity.ProductQR,
                CreatedBy= productEntity.CreatedBy,
                CreatedOn = productEntity.CreatedOn,
                ModifiedBy= productEntity.ModifiedBy,
                ModifiedOn = productEntity.ModifiedOn
            };
            return productBo;
        }
    }
}
