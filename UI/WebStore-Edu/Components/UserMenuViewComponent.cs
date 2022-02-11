using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Interfaces.Services;

namespace WebStore_Edu.Components
{
    public class UserMenuViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return User.Identity.IsAuthenticated
                ? View("Authorized")
                : View();
        }
    }
}
