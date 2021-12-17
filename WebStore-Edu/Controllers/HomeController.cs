using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Data;

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

        public IActionResult Employees()
        {
            return View(TestData.Employees);
        }

        public IActionResult EmployeeInfo(int id)
        {
            return View("EmployeeInfo", TestData.Employees.FirstOrDefault(e => e.Id == id));
        }
    }
}
