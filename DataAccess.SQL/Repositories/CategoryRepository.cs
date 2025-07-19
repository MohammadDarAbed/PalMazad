using DataAccess.Entities;
using Shared.Interfaces;
using System.Linq.Expressions;


namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryEntity>> GetAllCategoriesAsync();
        Task<CategoryEntity> GetByIdAsync(int id);
        Task CreateCategoryAsync(CategoryEntity entity);
        Task<CategoryEntity> UpdateCategoryAsync(CategoryEntity entity);
        Task DeleteCategoryAsync(int id);
        Task<bool> IsExistAsync(Expression<Func<CategoryEntity, bool>> filters);

    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<CategoryEntity> _categoryRepo;
        public CategoryRepository(IRepository<CategoryEntity> repo)
        {
            _categoryRepo = repo;
        }

        public Task<List<CategoryEntity>> GetAllCategoriesAsync()
        {
            return _categoryRepo.GetItemsAsync(filter: p => p.IsDeleted == false);
        }

        public Task<CategoryEntity> GetByIdAsync(int id)
        {
            return _categoryRepo.GetByIdAsync(id);
        }

        public Task CreateCategoryAsync(CategoryEntity entity)
        {
            return _categoryRepo.CreateAsync(entity);
        }

        public Task<CategoryEntity> UpdateCategoryAsync(CategoryEntity entity)
        {
            return _categoryRepo.UpdateAsync(entity);
        }

        public Task DeleteCategoryAsync(int id)
        {
            return _categoryRepo.DeleteAsync(id);
        }

        public Task<bool> IsExistAsync(Expression<Func<CategoryEntity, bool>> filters)
        {
            return _categoryRepo.ExistsAsync(new[] { filters });
        }

    }
}
