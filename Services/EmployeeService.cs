using Models;
using Services.Exceptions;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Services
{
    public class EmployeeService
    {
        private EmployeeStorage _employees;

        public EmployeeService(EmployeeStorage employee)
        {
            _employees = employee;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee.BirthDate > DateTime.Parse("31.12.2004"))
            {
                throw new AgeLimitException("Возраст клиента должен быть больше 18!");
            }

            if (employee.Passport == 0)
            {
                throw new PassportNullException("Нельзя добавить клиента без паспортных данных!");
            }

            _employees.Add(employee);
        }

        public List<Employee> GetEmployees(EmployeeFilter employeeFilter)
        {
            if (employeeFilter.FirstName != null && employeeFilter.LastName != null && employeeFilter.Patronymic != null)
            {
                return _employees.employees.Where(x => x.FirstName == employeeFilter.FirstName)
                .Where(x => x.LastName == employeeFilter.LastName)
                .Where(x => x.Patronymic == employeeFilter.Patronymic).ToList();
            }

            if (employeeFilter.Passport != 0)
            {
                return _employees.employees.Where(x => x.Passport == employeeFilter.Passport).ToList();
            }

            if (employeeFilter.Phone != 0)
            {
                return _employees.employees.Where(x => x.Phone == employeeFilter.Phone).ToList();
            }

            if (employeeFilter.BirthDayRange != null)
            {
                return _employees.employees
                    .Where(x => x.BirthDate >= employeeFilter.BirthDayRange[0] && x.BirthDate <= employeeFilter.BirthDayRange[1]).ToList();
            }

            return _employees.employees.Where(x => x.FirstName == employeeFilter.FirstName)
                .Where(x => x.LastName == employeeFilter.LastName)
                .Where(x => x.Patronymic == employeeFilter.Patronymic)
                .Where(x => x.Passport == employeeFilter.Passport)
                .Where(x => x.Phone == employeeFilter.Phone)
                .Where(x => x.BirthDate >= employeeFilter.BirthDayRange[0] && x.BirthDate <= employeeFilter.BirthDayRange[1])
                .Where(x => x.Contract == employeeFilter.Contract)
                .Where(x => x.Salary == employeeFilter.Salary)
                .ToList();
        }
    }
}