using MapsterMapper;
using WebStore_Edu.Data;
using WebStore_Edu.Models;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly IMapper _Mapper;

        public InMemoryEmployeesData(IMapper Mapper) => _Mapper = Mapper;

        public IEnumerable<Employee> GetAll() => TestData.Employees;

        public Employee? GetById(int Id) => TestData.Employees.FirstOrDefault(e => e.Id == Id);

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            employee.Id = GetNextId();
            TestData.Employees.Add(employee);
            return employee.Id;
        }

        public bool Update(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (GetById(employee.Id) is not { } empl) return false;

            //empl = _Mapper.Map<Employee>(employee);

            _Mapper.Map(employee, empl);

            return true;
        }

        public bool Delete(int Id)
        {
            if (GetById(Id) is not { } empl) return false;

            TestData.Employees.Remove(empl);

            return true;
        }

        private int _CurrentMaxId = TestData.Employees.Count;
        private int GetNextId() => ++_CurrentMaxId;
    }
}
