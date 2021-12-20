using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Checkout() => View();
        public IActionResult Cart() => View();
        public IActionResult Login() => View();
    }
}
