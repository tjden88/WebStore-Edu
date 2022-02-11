using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
