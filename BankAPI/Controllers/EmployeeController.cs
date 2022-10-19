using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ModelsDb;
using Services;
using Services.Filters;

namespace BankAPI.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService(new BankDbContext());
        }

        [HttpGet]
        public async Task<List<Employee>> GetEmployee(EmployeeFilter employee)
        {
            return await _employeeService.GetEmployees(employee);
        }

        [HttpDelete]
        public void DeleteEmployee(Guid id)
        {
            _employeeService.DeleteAsync(id);
        }

        [HttpPost]
        public void AddEmployee(Employee employee)
        {
            _employeeService.AddEmployeeAsync(employee);
        }

        [HttpPut]
        public void UpdateEmployee(Guid employeeId, Employee employee)
        {
            _employeeService.UpdateAsync(employeeId, employee);
        }
    }
}