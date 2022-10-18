using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.ModelsDb;
using Services.Filters;
using Services.Storage;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class EmployeeService
    {
        private BankDbContext _employees;

        public EmployeeService(BankDbContext employeeStorage)
        {
            _employees = employeeStorage;
        }

        public Task AddEmployee(Employee employee)
        {
            return Task.Run(() =>
            {
                var employeeDb = new EmployeeDb();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDb>());

                var mapper = new Mapper(config);

                employeeDb = mapper.Map<EmployeeDb>(employee);
                employeeDb.Id = Guid.NewGuid();

                if (employee.BirthDate > DateTime.Parse("31.12.2004"))
                {
                    throw new AgeLimitException("Возраст клиента должен быть больше 18!");
                }

                if (employee.Passport == 0)
                {
                    throw new PassportNullException("Нельзя добавить клиента без паспортных данных!");
                }

                if (!_employees.Employees.Contains(employeeDb))
                {
                    _employees.Employees.Add(employeeDb);

                    _employees.SaveChanges();
                }
            });
        }

        public Task Update(Guid employeeId, Employee employee)
        {
            return Task.Run(() =>
            {
                var employeeDb = new EmployeeDb();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDb>());

                var mapper = new Mapper(config);

                employeeDb = mapper.Map<EmployeeDb>(employee);
                employeeDb.Id = employeeId;

                IsEmployeeInDictionary(employeeDb);

                if (_employees.Employees.FirstOrDefault(x => x.Id == employeeId) != null)
                {
                    var getClient = _employees.Employees.FirstOrDefault(x => x.Id == employeeId);

                    _employees.Entry(getClient).CurrentValues.SetValues(employeeDb);

                    _employees.SaveChanges();
                }
            });
        }

        public Task Delete(Guid id)
        {
            return Task.Run(() =>
            {
                var employee = _employees.Employees.FirstOrDefault(x => x.Id == id);

                IsEmployeeInDictionary(employee);


                _employees.Employees.Remove(employee);

                _employees.SaveChanges();
            });
        }

        private async Task IsEmployeeInDictionaryAsync(EmployeeDb employee)
        {
            var findEmployee = await _employees.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);

            if (!_employees.Employees.Contains(findEmployee))
            {
                throw new ArgumentException("Сотрудник не найден!");
            }
        }

        public Task<List<Employee>> GetEmployees(EmployeeFilter employeeFilter)
        {
            return Task.Run(() =>
            {
                var employeeList = new List<Employee>();

                IQueryable<EmployeeDb> query = null;


                query = _employees.Employees.Select(t => t);

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

                if (employeeFilter.Id != Guid.Empty)
                {
                    query = query.Where(x => x.Id == employeeFilter.Id);
                }

                var employee = new List<Employee>();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDb, Employee>());

                var mapper = new Mapper(config);

                foreach (var item in query)
                {
                    employee.Add(mapper.Map<Employee>(item));
                }
                return employee;
            });
        }
    }
}
