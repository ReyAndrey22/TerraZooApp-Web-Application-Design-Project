using TerraZooApp.Models;
using TerraZooApp.Repositories;

namespace TerraZooApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public List<Employee> GetAll() => _repository.GetAll();

        public Employee? GetById(int id) => _repository.GetById(id);

        public void Create(Employee employee) => _repository.Create(employee);

        public void Update(Employee employee) => _repository.Update(employee);

        public void Delete(int id) => _repository.Delete(id);
    }
}
