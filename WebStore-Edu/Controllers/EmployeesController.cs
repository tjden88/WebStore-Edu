using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Data;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index() => View(TestData.Employees
            .Select(empl => new EmployeeViewModel()
            {
                Id = empl.Id,
                FirstName = empl.FirstName,
                LastName = empl.LastName,
                Patronymic = empl.Patronymic,
                Position = empl.Patronymic,
                Birthday = empl.Birthday,
            }
            ));

        public IActionResult EmployeeInfo(int id)
        {
            var empl = TestData.Employees.FirstOrDefault(e => e.Id == id);

            if (empl == null)
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = empl.Id,
                FirstName = empl.FirstName,
                LastName = empl.LastName,
                Patronymic = empl.Patronymic,
                Position = empl.Patronymic,
                Birthday = empl.Birthday,
            };

            return View(model);
        }

        public IActionResult EmployeeEdit(int id)
        {
            var empl = TestData.Employees.FirstOrDefault(e => e.Id == id);

            if (empl == null)
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = empl.Id,
                FirstName = empl.FirstName,
                LastName = empl.LastName,
                Patronymic = empl.Patronymic,
                Position = empl.Patronymic,
                Birthday = empl.Birthday,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EmployeeEdit(EmployeeViewModel item)
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
        public IActionResult DeleteEmployee(EmployeeViewModel item)
        {
            if (TestData.Employees.FirstOrDefault(e => e.Id == item.Id) is { } empl)
            {
                TestData.Employees.Remove(empl);
            }
            return RedirectToAction("Index");
        }
    }
}
