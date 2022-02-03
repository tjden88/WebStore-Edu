using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Interfaces.Services;

namespace WebSore_Edu.WebAPI.Controllers
{
    [Route(ApiAddresses.Employees)]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        [HttpGet]
        public IActionResult Get() => Ok(_EmployeesData.GetAll());

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var employee = _EmployeesData.GetById(Id);

            if(employee is null) return NotFound();

            return Ok(employee);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Employee Employee)
        {
            var id = _EmployeesData.Add(Employee);

            return CreatedAtAction(nameof(Get), new { Id = id }, Employee);
        }


        [Authorize(Roles = Role.Administrators)]
        [HttpPut]
        public IActionResult Update(Employee Employee)
        {
            var result = _EmployeesData.Update(Employee);
            return result
                ? Ok(true)
                : NotFound(false);
        }


        [Authorize(Roles = Role.Administrators)]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var result = _EmployeesData.Delete(Id);
            return result
                ? Ok(true)
                : NotFound(false);
        }
    }
}
