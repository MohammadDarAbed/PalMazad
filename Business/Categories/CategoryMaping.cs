
using Business.Categories;
using DataAccess.Entities;
using DataAccess.Models;

namespace Business.Categories
{
    public static class CategoryMaping
    {
        public static CategoryEntity? MapModelToEntity(this CategoryModel categoryModel)
        {
            if (categoryModel == null) return null;
            var categoryEntity = new CategoryEntity
            {
                Name = categoryModel.Name,
                CreatedBy = "-",
                ModifiedBy = "-",
                CreatedOn = DateTimeOffset.Now,
            };
            return categoryEntity;
        }

        public static CategoryModelBo? MapEntityToBo(this CategoryEntity categoryEntity)
        {
            if (categoryEntity == null) return null;
            var categoryBo = new CategoryModelBo
            {
                Id = categoryEntity.Id,
                Name = categoryEntity.Name,
                IsDeleted = categoryEntity.IsDeleted,
                CreatedBy= categoryEntity.CreatedBy,
                CreatedOn = categoryEntity.CreatedOn,
                ModifiedBy= categoryEntity.ModifiedBy,
                ModifiedOn = categoryEntity.ModifiedOn
            };
            return categoryBo;
        }
    }
}
