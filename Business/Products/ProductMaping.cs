
using Business.Users;
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
                CategoryId = productModel.CategoryId ?? 0,
                Price = productModel.Price,
                ProductQR = productModel.ProductQR,
                Condition = productModel.Condition,
                IsHiddenSellerInfo = productModel.IsHiddenSellerInfo,
                IsPublished = productModel.IsPublished,                
                CreatedBy = "-",
                Description = productModel.Description,
                SellerId = productModel.SellerId,
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
                Description = productEntity.Description ?? "",
                Category = productEntity.Category.Name,
                Condition = Enum.GetName(typeof(ProductCondition), productEntity.Condition) ?? "",
                IsHiddenSellerInfo = productEntity.IsHiddenSellerInfo,
                IsDeleted = productEntity.IsDeleted,
                Price = productEntity.Price,
                ProductQR = productEntity.ProductQR,
                Seller = productEntity.Seller.MapEntityToBo(),
                CreatedBy= productEntity.CreatedBy,
                CreatedOn = productEntity.CreatedOn,
                ModifiedBy= productEntity.ModifiedBy,
                ModifiedOn = productEntity.ModifiedOn
            };
            return productBo;
        }
    }
}
