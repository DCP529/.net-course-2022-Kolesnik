using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.ModelsDb;
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

        public void AddEmployee(EmployeeDb employee)
        {
            if (employee.BirthDate > DateTime.Parse("31.12.2004"))
            {
                throw new AgeLimitException("Возраст клиента должен быть больше 18!");
            }

            if (employee.Passport == 0)
            {
                throw new PassportNullException("Нельзя добавить клиента без паспортных данных!");
            }

            if (!_employees.Data.Employees.Contains(employee))
            {
                _employees.Add(employee);
            }
        }

        public void Update(EmployeeDb employee)
        {
            IsEmployeeInDictionary(employee);

            _employees.Update(employee.Id, employee);
        }

        public void Delete(EmployeeDb employee)
        {
            IsEmployeeInDictionary(employee);

            _employees.Delete(employee.Id);
        }

        private void IsEmployeeInDictionary(EmployeeDb employee)
        {
            var findEmployee = _employees.Data.Employees.FirstOrDefault(x => x.Id == employee.Id);

            if (!_employees.Data.Employees.Contains(findEmployee))
            {
                throw new ArgumentException("Сотрудник не найден!");
            }
        }

        public List<EmployeeDb> GetEmployees(EmployeeFilter employeeFilter)
        {
            var query = _employees.Data.Employees.Select(t => t);

            if (employeeFilter.FirstName != null && employeeFilter.LastName != null && employeeFilter.Patronymic != null)
            {
                query = query.Where(x => x.FirstName == employeeFilter.FirstName)
                    .Where(x => x.LastName == employeeFilter.LastName)
                    .Where(x => x.Patronymic == employeeFilter.Patronymic);
            }

            if (employeeFilter.Passport != 0)
            {
                query = query.Where(x => x.Passport == employeeFilter.Passport);
            }

            if (employeeFilter.Phone != 0)
            {
                query = query.Where(x => x.Phone == employeeFilter.Phone);
            }

            if (employeeFilter.BirthDayRange != null)
            {
                query = query.Where(x => x.BirthDate >= employeeFilter.BirthDayRange.Item1 && x.BirthDate <= employeeFilter.BirthDayRange.Item2);
            }

            if (employeeFilter.Id != null)
            {
                query = query.Where(x => x.Id == employeeFilter.Id);
            }
            

            return query.ToList();
        }
    }
}
