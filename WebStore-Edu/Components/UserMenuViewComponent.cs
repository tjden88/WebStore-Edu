using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Components
{
    public class UserMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity.IsAuthenticated 
            ? View("Authorized")
            : View();
    }
}
