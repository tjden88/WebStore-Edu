using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;

        public CartController(ICartService CartService)
        {
            _CartService = CartService;
        }
        public IActionResult Index()
        {
            return View(_CartService.CreateViewModel());
        }
    }
}
