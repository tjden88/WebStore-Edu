using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Models;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMapper _Mapper;
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IMapper Mapper, IEmployeesData EmployeesData)
        {
            _Mapper = Mapper;
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index() => View(_EmployeesData
            .GetAll()
            .Select(empl => _Mapper.Map<EmployeeViewModel>(empl)));

        public IActionResult EmployeeInfo(int id)
        {
            var empl = _EmployeesData.GetById(id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
        }

        public IActionResult EmployeeEdit(int id)
        {
            var empl = _EmployeesData.GetById(id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
        }

        [HttpPost]
        public IActionResult EmployeeEdit(EmployeeViewModel item)
        {
            var employee = _Mapper.Map<Employee>(item);

            if (!_EmployeesData.Update(employee))
                return NotFound();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult DeleteEmployee(EmployeeViewModel item)
        {
            if (!_EmployeesData.Delete(item.Id))
                return NotFound();

            return RedirectToAction("Index");
        }
    }
}
