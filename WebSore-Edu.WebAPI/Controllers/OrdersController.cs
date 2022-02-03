using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.DTO;
using WebStore_Edu.Domain.DTO.Orders;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;

namespace WebSore_Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        private readonly IMapper _Mapper;

        public OrdersController(IOrderService OrderService, IMapper Mapper)
        {
            _OrderService = OrderService;
            _Mapper = Mapper;
        }

        [HttpGet("list/{UserName}")]
        public async Task<IActionResult> GetUserOrders(string UserName, [FromServices] UserManager<User> UserManager)
        {
            var orders = await _OrderService.GetUserOrdersAsync(await UserManager.FindByNameAsync(UserName));
            return Ok(orders.Adapt<IEnumerable<OrderDTO>>());
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrder(int Id)
        {
            var order = await _OrderService.GetOrderAsync(Id);

            return order == null 
                ? NotFound() 
                : Ok(order.Adapt<OrderDTO>());
        }

        [HttpPost("add/{UserName}")]
        public async Task<IActionResult> CreateOrder(string UserName, CreateOrderDTO Order, [FromServices] UserManager<User> UserManager)
        {
            var user = await UserManager.FindByNameAsync(UserName);
            var cart = _Mapper.Map<CartViewModel>(Order);

            var order = await _OrderService.CreateOrderAsync(user, cart, Order.OrderModel);
            var orderDTO = order.Adapt<OrderDTO>();
            return Ok(orderDTO);
        }
    }
}
