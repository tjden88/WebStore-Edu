using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Data;

namespace WebStore_Edu.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index() => View(TestData.Employees);

        public IActionResult EmployeeInfo(int id)
        {
            return View("EmployeeInfo", TestData.Employees.FirstOrDefault(e => e.Id == id));
        }
    }
}
