using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Models;
using TerraZooApp.Services;

namespace TerraZooApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        public IActionResult IndexEmployee()
        {
            var employees = _service.GetAll();
            return View(employees);
        }

        public IActionResult AddEmployee() => View();

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _service.Create(employee);
                return RedirectToAction("IndexEmployee");
            }
            return View(employee);
        }

        public IActionResult UpdateEmployee(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _service.Update(employee);
                return RedirectToAction("IndexEmployee");
            }
            return View(employee);
        }

        public IActionResult DeleteEmployee(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteEmployeeConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("IndexEmployee");
        }

        public IActionResult ListEmployees()
        {
            var employees = _service.GetAll();
            return View(employees); 
        }
    }
}
