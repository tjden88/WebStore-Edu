using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Interfaces.Services;

namespace WebStore_Edu.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _CartService;

        public CartViewComponent(ICartService CartService) => _CartService = CartService;

        public IViewComponentResult Invoke() => View(_CartService.ProductsCount());
    }
}
