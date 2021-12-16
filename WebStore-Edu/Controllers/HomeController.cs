using Microsoft.AspNetCore.Mvc;

namespace WebStore_Edu.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //return View("Index"); // Если названия в папке Views не совпадают
            // return View("~/Views/Home/Index.cshtml"); // Полный путь
       }
    }
}
