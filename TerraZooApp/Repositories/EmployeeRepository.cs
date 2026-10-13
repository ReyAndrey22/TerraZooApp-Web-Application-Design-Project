using TerraZooApp.Data;
using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TerraZooAppDbContext _context;

        public EmployeeRepository(TerraZooAppDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll() => _context.Employees.ToList();

        public Employee? GetById(int id) => _context.Employees.Find(id);

        public void Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
        }
    }
}
