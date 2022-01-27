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

        public IActionResult Add(int Id, int Quantity = 1)
        {
            _CartService.Add(Id, Quantity);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int Id)
        {
            _CartService.Remove(Id);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            _CartService.Clear();
            return RedirectToAction("Index");
        }
    }
}
