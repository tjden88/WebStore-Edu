using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.DTO;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Interfaces.Services;

namespace WebSore_Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrdersController(IOrderService OrderService) => _OrderService = OrderService;

        [HttpGet("list/{UserName}")]
        public async Task<IActionResult> GetUserOrders(string UserName, [FromServices] UserManager<User> UserManager)
        {
            var orders = await _OrderService.GetUserOrdersAsync(await UserManager.FindByNameAsync(UserName));
            return Ok(orders);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrder(int Id)
        {
            var order = await _OrderService.GetOrderAsync(Id);

            return order == null 
                ? NotFound() 
                : Ok(order);
        }

        [HttpPost("add/{UserName}")]
        public async Task<IActionResult> CreateOrder(string UserName, CreateOrderDTO Order, [FromServices] UserManager<User> UserManager)
        {
            var user = await UserManager.FindByNameAsync(UserName);
            var order = await _OrderService.CreateOrderAsync(user, Order.Cart, Order.OrderModel);
            return Ok(order);
        }
    }
}
