using Business.CartShopping;
using Business.Managers;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet("{cartId}")]
        public async Task<ActionResult<List<CartItemModelBo>>> GetCartItems(int cartId)
        {
            var items = await _cartManager.GetCartItemsAsync(cartId);
            if (items == null)
                return NotFound();

            return Ok(items);
        }

        [HttpPost("items/batch")]
        public async Task<IActionResult> AddItemsToCart([FromBody] AddCartItemsRequest request)
        {
            //int userId = int.Parse(User.FindFirst("sub")?.Value ?? "0");
            //if (userId == 0)
            //    return Unauthorized();

            var success = await _cartManager.AddItemsToCartAsync(1, request.Items);// TODO: UserId
            if (!success)
                return BadRequest("One or more products do not exist");

            return Ok("Items added to cart");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var cart = await _cartManager.GetCartByUserIdAsync(userId);

            if (cart == null)
                return NotFound($"Cart not found for user id {userId}");

            return Ok(cart);
        }
    }
}
