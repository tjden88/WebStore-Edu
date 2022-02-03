using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Interfaces.Services;

namespace WebSore_Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrdersController(IOrderService OrderService) => _OrderService = OrderService;
    }
}
