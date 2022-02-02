using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Interfaces.Services;

namespace WebStore_Edu.Components
{
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly ICartService _CartService;

        public UserMenuViewComponent(ICartService CartService) => _CartService = CartService;

        public IViewComponentResult Invoke()
        {
            ViewBag.CartCount = _CartService.ProductsCount();
            return User.Identity.IsAuthenticated
                ? View("Authorized")
                : View();
        }
    }
}
