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


        public IActionResult EmployeeInfo(int Id)
        {
            var empl = _EmployeesData.GetById(Id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
        }


        public IActionResult EmployeeEdit(int Id)
        {
            var empl = _EmployeesData.GetById(Id);

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


        public IActionResult DeleteQuestion(int Id)
        {
            var empl = _EmployeesData.GetById(Id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
        }


        [HttpPost]
        public IActionResult DeleteEmployee(int Id)
        {
            if (!_EmployeesData.Delete(Id))
                return NotFound();

            return RedirectToAction("Index");
        }
    }
}
