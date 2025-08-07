using Business.Orders;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using PalMazadStore.Migrations;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly AppDbContext _context;

        public OrderController(IOrderManager orderManager, AppDbContext context)
        {
            _orderManager = orderManager;
            _context = context;

        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderModel orderModel)
        {
            var order = await _orderManager.CreateOrder(orderModel);
            return Ok(order);
        }

        [HttpPost]
        [Route("{cartId:min(1)}/checkout")]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<OrderDto>> CreateOrderFromCart([FromBody] CartCheckoutModel orderModel, int cartId)
        {
            var order = await _orderManager.CreateOrderFromCart(orderModel, cartId);
            return Ok(order);
        }

        [HttpGet]
        [Route("orders")]
        [ProducesResponseType(typeof(List<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderManager.GetOrders();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{orderId:min(1)}/GetById")]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderManager.GetOrderById(orderId);
            return Ok(order);
        }

        [HttpPut]
        [Route("{id:min(1)}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderModel orderModel)
        {
            var order = await _orderManager.UpdateOrder(id, orderModel);
            return Ok(order);
        }

        [HttpDelete]
        [Route("{id:min(1)}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderManager.DeleteOrder(id);
            return NoContent();
        }

        [HttpPut("{id}/payment-status")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, [FromBody] UpdatePaymentStatusModel model)
        {
            await _orderManager.UpdatePaymentStatusAsync(id, model);
            return Ok();
        }
    }
}
