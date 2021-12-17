using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Data;
using WebStore_Edu.Models;

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

            // Так не работает. Почему? Есть ли более изящный способ вернуть представление другого контроллера?
            //if (empl == null)
            //    return new HomeController().NotFoundPage();

            return View(empl);
        }

        public IActionResult EmployeeEdit(int id)
        {
            var empl = TestData.Employees.FirstOrDefault(e => e.Id == id);

            if (empl == null)
                return NotFound();

            return View(empl);
        }

        [HttpPost]
        public IActionResult EmployeeEdit(Employee item)
        {
            if (TestData.Employees.FirstOrDefault(e => e.Id == item.Id) is { } empl)
            {
                empl.FirstName = item.FirstName;
                empl.LastName = item.LastName;
                empl.Patronymic = item.Patronymic;
                empl.Birthday = item.Birthday;
                empl.Position = item.Position;
            }
            // return Index(); Так тоже не работает почему-то...
            return View("Index", TestData.Employees);
        }
    }
}
