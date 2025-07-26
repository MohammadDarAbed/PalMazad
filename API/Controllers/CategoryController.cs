using Business.Categories;
using Business.Categories;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using PalMazadStore.Migrations;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _prodductManager;
        private readonly AppDbContext _context;

        public CategoryController(ICategoryManager prodductManager, AppDbContext context)
        {
            _prodductManager = prodductManager;
            _context = context;

        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CategoryModelBo), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<CategoryModelBo>> CreateCategory([FromBody] CategoryModel categoryModel)
        {
            var category = await _prodductManager.CreateCategory(categoryModel);
            return Ok(category);
        }

        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(typeof(List<CategoryModelBo>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _prodductManager.GetCategories();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{categoryId:min(1)}/GetById")]
        [ProducesResponseType(typeof(CategoryModelBo), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _prodductManager.GetCategoryById(categoryId);
            return Ok(category);
        }

        [HttpPut]
        [Route("{id:min(1)}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryModel categoryModel)
        {
            var category = await _prodductManager.UpdateCategory(id, categoryModel);
            return Ok(category);
        }

        [HttpDelete]
        [Route("{id:min(1)}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _prodductManager.DeleteCategory(id);
            return NoContent();
        }
    }
}

