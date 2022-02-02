using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Services.Interfaces;
using WebStore_Edu.ViewModels;

namespace WebStore_Edu.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IMapper _Mapper;
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IMapper Mapper, IEmployeesData EmployeesData)
        {
            _Mapper = Mapper;
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index() => View(_Mapper.Map<IEnumerable<EmployeeViewModel>>(_EmployeesData.GetAll()));


        public IActionResult EmployeeInfo(int Id)
        {
            var empl = _EmployeesData.GetById(Id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
        }

        [Authorize(Roles = Role.Administrators)]
        public IActionResult EmployeeEdit(int? Id)
        {
            if (Id is null)
                return View(new EmployeeViewModel());


            var empl = _EmployeesData.GetById((int) Id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
        }

        [Authorize(Roles = Role.Administrators)]
        [HttpPost]
        public IActionResult EmployeeEdit(EmployeeViewModel model)
        {
            var age = model.Age;
            if(age < 18)
            {
                ModelState.AddModelError(nameof(model.Birthday), "Возраст должен быть не менее 18 лет");
            }

            if (!ModelState.IsValid)
                return View(model);

            var employee = _Mapper.Map<Employee>(model);

            if (employee.Id == 0)
                _EmployeesData.Add(employee);
            else if (!_EmployeesData.Update(employee))
                return NotFound();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Administrators)]
        public IActionResult DeleteQuestion(int Id)
        {
            var empl = _EmployeesData.GetById(Id);

            if (empl == null)
                return NotFound();

            return View(_Mapper.Map<EmployeeViewModel>(empl));
        }

        [Authorize(Roles = Role.Administrators)]
        [HttpPost]
        public IActionResult DeleteEmployee(int Id)
        {
            if (!_EmployeesData.Delete(Id))
                return NotFound();

            return RedirectToAction("Index");
        }
    }
}
