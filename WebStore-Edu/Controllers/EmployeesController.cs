using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Data;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMapper _Mapper;

        public EmployeesController(IMapper Mapper) => _Mapper = Mapper;

        public IActionResult Index() => View(TestData.Employees
            .Select(empl => _Mapper.Map<EmployeeViewModel>(empl)));

        public IActionResult EmployeeInfo(int id)
        {
            var empl = TestData.Employees.FirstOrDefault(e => e.Id == id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
        }

        public IActionResult EmployeeEdit(int id)
        {
            var empl = TestData.Employees.FirstOrDefault(e => e.Id == id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
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
