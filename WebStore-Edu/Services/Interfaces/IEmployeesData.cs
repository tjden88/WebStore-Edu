using WebStore_Edu.Domain.Entityes;

namespace WebStore_Edu.Services.Interfaces
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
