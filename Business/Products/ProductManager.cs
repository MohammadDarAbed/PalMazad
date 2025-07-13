
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using PalMazadStore.Migrations;
using Shared.Interfaces;

namespace Business.Products
{
    public interface IProductManager
    {
        Task<List<ProductBo>> GetProducts();
        Task<ProductBo> GetProductById(int productId);
        Task<ProductBo> CreateProduct(ProductModel productModel);
        Task<ProductBo> UpdateProduct(int productId, ProductModel productModel);
        Task DeleteProduct(int id);

    }

    public class ProductManager(IProductRepository _productRepo) : IProductManager
    {        
        public async Task<List<ProductBo>> GetProducts()
        {
            var products = await _productRepo.GetAllProductsAsync();
            return products.Select(p => p.MapEntityToBo()).ToList();

        }

        public async Task<ProductBo> CreateProduct(ProductModel productModel)
        {
            var entity = productModel.MapModelToEntity();
            await _productRepo.CreateProductAsync(entity);
            return entity.MapEntityToBo();
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepo.DeleteProductAsync(id);
        }

        public async Task<ProductBo> GetProductById(int productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            return product.MapEntityToBo();
        }

        public async Task<ProductBo> UpdateProduct(int productId, ProductModel productModel)
        {
            var entity = productModel.MapModelToEntity();
            entity.Id = productId;
            var updatedProduct = await _productRepo.UpdateProductAsync(entity);
            return updatedProduct.MapEntityToBo();
        }
    }
}
