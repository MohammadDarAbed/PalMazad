
using Business.Categories;
using Business.Shared;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Business.Categories
{
    public interface ICategoryManager
    {
        Task<List<CategoryModelBo>> GetCategories();
        Task<CategoryModelBo> GetCategoryById(int categoryId);
        Task<CategoryModelBo> CreateCategory(CategoryModel categoryModel);
        Task<CategoryModelBo> UpdateCategory(int categoryId, CategoryModel categoryModel);
        Task DeleteCategory(int id);
    }

    public class CategoryManager(ICategoryRepository _categoryRepo) : ICategoryManager
    {        
        public async Task<List<CategoryModelBo>> GetCategories()
        {
            var categories = await _categoryRepo.GetAllCategoriesAsync();
            return categories.Select(p => p.MapEntityToBo()).ToList();

        }

        public async Task<CategoryModelBo> GetCategoryById(int categoryId)
        {
            await CheckIfNotExists(categoryId);
            var category = await _categoryRepo.GetByIdAsync(categoryId);
            Validations.CheckIfEntityDeleted(category.IsDeleted, categoryId, "Category");
            return category.MapEntityToBo();
        }

        public async Task<CategoryModelBo> CreateCategory(CategoryModel categoryModel)
        {
            var entity = categoryModel.MapModelToEntity();
            await _categoryRepo.CreateCategoryAsync(entity);
            return entity.MapEntityToBo();
        }

        public async Task DeleteCategory(int id)
        {
            await _categoryRepo.DeleteCategoryAsync(id);
        }

        public async Task<CategoryModelBo> UpdateCategory(int categoryId, CategoryModel categoryModel)
        {
            await CheckIfNotExists(categoryId);
            var entity = categoryModel.MapModelToEntity();
            entity.Id = categoryId;
            var updatedCategory = await _categoryRepo.UpdateCategoryAsync(entity);
            return updatedCategory.MapEntityToBo();
        }

        // Helpers:

        private async Task CheckIfNotExists(int id)
        {
            var isExist = await _categoryRepo.IsExistAsync(f => f.Id == id);
            if (!isExist)
            {
                // if record delete
                ExceptionManager.ThrowItemNotFoundException("Category", id);
            }
        }
    }
}
