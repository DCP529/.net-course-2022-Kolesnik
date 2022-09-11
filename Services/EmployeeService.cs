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
            IEnumerable<Employee> query = null;

            if (employeeFilter.FirstName != null && employeeFilter.LastName != null && employeeFilter.Patronymic != null)
            {
                query = _employees.employees.Where(x => x.FirstName == employeeFilter.FirstName)
                .Where(x => x.LastName == employeeFilter.LastName)
                .Where(x => x.Patronymic == employeeFilter.Patronymic);
            }

            if (employeeFilter.Passport != 0 && query != null)
            {
                return _employees.employees.Where(x => x.Passport == employeeFilter.Passport).ToList();
            }
            else if (employeeFilter.Passport != 0)
            {
                query = query.Intersect(_employees.employees.Where(x => x.Passport == employeeFilter.Passport));
            }

            if (employeeFilter.Phone != 0 && query != null)
            {
                return _employees.employees.Where(x => x.Phone == employeeFilter.Phone).ToList();
            }
            else if (employeeFilter.Phone != 0)
            {
                query = query.Intersect(_employees.employees.Where(x => x.Phone == employeeFilter.Phone));
            }

            if (employeeFilter.BirthDayRange != null && query != null)
            {
                return _employees.employees
                    .Where(x => x.BirthDate >= employeeFilter.BirthDayRange.Item1 && x.BirthDate <= employeeFilter.BirthDayRange.Item2).ToList();
            }
            else if (employeeFilter.BirthDayRange != null)
            {
                query = query.Intersect(_employees.employees
                    .Where(x => x.BirthDate >= employeeFilter.BirthDayRange.Item1 && x.BirthDate <= employeeFilter.BirthDayRange.Item2));
            }

            if (query != null)
            {
                return query.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
