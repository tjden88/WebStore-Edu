using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;

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

        public IActionResult Add(int Id, int Quantity = 1, string? ReturnUrl = null)
        {
            _CartService.Add(Id, Quantity);
            return ReturnUrl is null 
                ? RedirectToAction("Index")
                : LocalRedirect(ReturnUrl);
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

        public IActionResult Checkout() => View(new CartOrderViewModel {Cart = _CartService.CreateViewModel()});


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderVievModel Model, [FromServices] IOrderService OrderService, [FromServices] UserManager<User> UserManager)
        {
            if (!ModelState.IsValid)
                return View(new CartOrderViewModel
                {
                    Cart = _CartService.CreateViewModel(),
                    Order = Model
                });

            var order = await OrderService.
                CreateOrderAsync(await UserManager.FindByNameAsync(User.Identity!.Name),
                    _CartService.CreateViewModel(),
                    Model);

            _CartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { OrderId = order.Id });
        }

        public IActionResult OrderConfirmed(int OrderId) => View(OrderId);


    }
}
