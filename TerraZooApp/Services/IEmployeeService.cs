using TerraZooApp.Models;

namespace TerraZooApp.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee? GetById(int id);
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
    }
}
