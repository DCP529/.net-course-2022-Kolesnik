using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services.Filters;
using Services.Storage;

namespace Services
{
    public class EmployeeService
    {
        private IEmployeeStorage _employees = new EmployeeStorage();

        public EmployeeService(IEmployeeStorage employeeStorage)
        {
            _employees = employeeStorage;
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

        public void Update(Employee employee)
        {
            _employees.Update(employee);
        }

        public void Delete(Employee employee)
        {
            _employees.Delete(employee);
        }

        public List<Employee> GetEmployees(EmployeeFilter employeeFilter)
        {
            IEnumerable<Employee> query = _employees.Data.Select(t => t);

            if (employeeFilter.FirstName != null && employeeFilter.LastName != null && employeeFilter.Patronymic != null)
            {
                query.Where(x => x.FirstName == employeeFilter.FirstName)
                    .Where(x => x.LastName == employeeFilter.LastName)
                    .Where(x => x.Patronymic == employeeFilter.Patronymic);
            }

            if (employeeFilter.Passport != 0)
            {
                query.Where(x => x.Passport == employeeFilter.Passport);
            }

            if (employeeFilter.Phone != 0)
            {
                query.Where(x => x.Phone == employeeFilter.Phone);
            }

            if (employeeFilter.BirthDayRange != null)
            {
                query.Where(x => x.BirthDate >= employeeFilter.BirthDayRange.Item1 && x.BirthDate <= employeeFilter.BirthDayRange.Item2);
            }

            return query.ToList();
        }
    }
}