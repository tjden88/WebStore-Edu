using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Data;

namespace WebStore_Edu.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index() => View(TestData.Employees);

        public IActionResult EmployeeInfo(int id)
        {
            var empl = TestData.Employees.FirstOrDefault(e => e.Id == id);

            if (empl == null)
                return NotFound();

            return View("EmployeeInfo", empl);
        }
    }
}
