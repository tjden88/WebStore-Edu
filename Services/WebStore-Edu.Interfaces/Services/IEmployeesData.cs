using WebStore_Edu.Domain.Entityes;

namespace WebStore_Edu.Interfaces.Services
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee? GetById(int Id);

        int Add(Employee employee);

        bool Update(Employee employee);

        bool Delete(int Id);
    }
}
