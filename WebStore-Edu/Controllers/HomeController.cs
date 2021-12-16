using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Привет!");
            //return View();
        }
    }
}
