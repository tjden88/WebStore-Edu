using System.Net.Http.Json;
using WebSore_Edu.WebAPI;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Interfaces.Services;
using WebStore_Edu.WebAPI.Clients.Base;

namespace WebStore_Edu.WebAPI.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(HttpClient HttpHttp) : base(HttpHttp, ApiAddresses.Employees)
        {
        }

        public IEnumerable<Employee> GetAll() => Get<IEnumerable<Employee>>(Address) ?? Enumerable.Empty<Employee>();

        public Employee? GetById(int Id) => Get<Employee?>($"{Address}/{Id}");

        public int Add(Employee employee)
        {
            var response = Post(Address, employee);
            var added = response.Content.ReadFromJsonAsync<Employee>().Result;

            return added?.Id ?? -1;
        }

        public bool Update(Employee employee) => Put(Address, employee).IsSuccessStatusCode;

        public bool Delete(int Id) => Delete($"{Address}/{Id}").IsSuccessStatusCode;
    }
}
