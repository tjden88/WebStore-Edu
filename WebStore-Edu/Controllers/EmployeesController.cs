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
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult DeleteEmployee(Employee item)
        {
            if (TestData.Employees.FirstOrDefault(e => e.Id == item.Id) is { } empl)
            {
                TestData.Employees.Remove(empl);
            }
            return RedirectToAction("Index");
        }
    }
}
