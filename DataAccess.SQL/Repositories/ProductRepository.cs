
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAllProductsAsync();
        Task<ProductEntity> GetByIdAsync(int id);
        Task CreateProductAsync(ProductEntity entity);
        Task<ProductEntity> UpdateProductAsync(ProductEntity entity);
        Task DeleteProductAsync(int id);
        Task<bool> IsExistAsync(Expression<Func<ProductEntity, bool>> filters);

    }
    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<ProductEntity> _productRepo;
        public ProductRepository(IRepository<ProductEntity> repo) 
        {
            _productRepo = repo;
        }

        public async Task<List<ProductEntity>> GetAllProductsAsync()
        {
            var products = await _productRepo.GetItemsAsync(
                filter: p => p.IsDeleted == false,
                includes: query => query
                .Include(p => p.Category)
                .Include(p => p.Seller)
                 );
            return products;
        }

        public async Task<ProductEntity> GetByIdAsync(int id)
        {
            return await _productRepo.GetByIdAsync(id, 
                includes:  query => query
                .Include(p => p.Category)
                .Include(p => p.Seller));
        }

        public async Task CreateProductAsync(ProductEntity entity)
        {
            await _productRepo.CreateAsync(entity);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepo.DeleteAsync(id);
        }

        public async Task<ProductEntity> UpdateProductAsync(ProductEntity entity)
        {
           return await _productRepo.UpdateAsync(entity);
        }

        public async Task<bool> IsExistAsync(Expression<Func<ProductEntity, bool>> filters)
        {
            return await _productRepo.ExistsAsync(new[] { filters });
        }
    }
}
