using Business.Products;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using PalMazadStore.Migrations;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _prodductManager;
        private readonly AppDbContext _context;

        public ProductController(IProductManager prodductManager, AppDbContext context)
        {
            _prodductManager = prodductManager;
            _context = context;

        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ProductBo), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ProductBo>> CreateProduct([FromBody] ProductModel productModel)
        {
            var product = await _prodductManager.CreateProduct(productModel);
            return Ok(product);
        }

        [HttpGet]
        [Route("products")]
        [ProducesResponseType(typeof(List<ProductBo>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _prodductManager.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        [Route("{productId:min(1)}/GetById")]
        [ProducesResponseType(typeof(ProductBo), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _prodductManager.GetProductById(productId);
            return Ok(product);
        }

        [HttpPut]
        [Route("{id:min(1)}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductModel productModel)
        {
            var product = await _prodductManager.UpdateProduct(id, productModel);
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:min(1)}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _prodductManager.DeleteProduct(id);
            return NoContent();
        }
    }
}
