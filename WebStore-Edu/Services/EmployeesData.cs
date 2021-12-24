using MapsterMapper;
using WebStore_Edu.Data;
using WebStore_Edu.Models;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly IMapper _Mapper;
        private readonly ILogger<InMemoryEmployeesData> _Logger;

        public InMemoryEmployeesData(IMapper Mapper, ILogger<InMemoryEmployeesData> Logger)
        {
            _Mapper = Mapper;
            _Logger = Logger;
        }

        public IEnumerable<Employee> GetAll() => TestData.Employees;

        public Employee? GetById(int Id) => TestData.Employees.FirstOrDefault(e => e.Id == Id);

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            employee.Id = GetNextId();
            TestData.Employees.Add(employee);
            _Logger.LogInformation("Добавлен новый сотрудник: {0}", employee);
            return employee.Id;
        }

        public bool Update(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (GetById(employee.Id) is not { } empl)
            {
                _Logger.LogWarning("Попытка изменить несуществующего сотрудника с id:{0}", employee.Id);
                return false;
            }

            //empl = _Mapper.Map<Employee>(employee);

            _Mapper.Map(employee, empl);
            _Logger.LogInformation("Изменён сотрудник: {0}", empl);


            return true;
        }

        public bool Delete(int Id)
        {
            if (GetById(Id) is not { } empl)
            {
                _Logger.LogWarning("Попытка удалить несуществующего сотрудника с id:{0}", Id);
                return false;
            }

            TestData.Employees.Remove(empl);
            _Logger.LogInformation("Удалён сотрудник: {0}", empl);

            return true;
        }

        private int _CurrentMaxId = TestData.Employees.Count;
        private int GetNextId() => ++_CurrentMaxId;
    }
}
