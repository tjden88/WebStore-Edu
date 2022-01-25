using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Authorize() => View();

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
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
