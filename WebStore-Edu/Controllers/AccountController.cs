using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Authorize() => View();

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout() => RedirectToAction("Index", "Home");

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
