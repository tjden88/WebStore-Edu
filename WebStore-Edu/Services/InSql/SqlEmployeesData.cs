using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Services.InSql
{
    public class SqlEmployeesData : IEmployeesData

    {
        private readonly WebStoreDb _Db;
        private readonly IMapper _Mapper;
        private readonly ILogger<SqlEmployeesData> _Logger;

        public SqlEmployeesData(WebStoreDb Db, IMapper Mapper, ILogger<SqlEmployeesData> Logger)
        {
            _Db = Db;
            _Mapper = Mapper;
            _Logger = Logger;
        }

        public IEnumerable<Employee> GetAll() => _Db.Employees;

        public Employee? GetById(int Id) => _Db.Employees.FirstOrDefault(e => e.Id == Id);

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            _Db.Entry(employee).State = EntityState.Added;
            _Db.SaveChanges();
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

            _Db.Entry(employee).State = EntityState.Modified;
            _Db.SaveChanges();

            _Logger.LogInformation("Изменён сотрудник: {0}", empl);

            return true;
        }

        public bool Delete(int Id)
        {
            if (GetById(Id) is not { } employee)
            {
                _Logger.LogWarning("Попытка удалить несуществующего сотрудника с id:{0}", Id);
                return false;
            }

            _Db.Entry(employee).State = EntityState.Deleted;
            _Db.SaveChanges();

            _Logger.LogInformation("Удалён сотрудник: {0}", employee);

            return true;
        }
    }
}
