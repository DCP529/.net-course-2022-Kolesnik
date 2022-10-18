using Models;
using Services;
using Services.Exceptions;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.ModelsDb;
using Xunit;

namespace ServicesTests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void Filter_EmployeeTest()
        {
            //Arange

            var employeeService = new EmployeeService(new BankDbContext());

            var ts = new TestDataGenerator();


            //Act

            List<Employee> employees = ts.GenerateListEmployee();

            foreach (var employee in employees)
            {
                employeeService.AddEmployee(employee);
            }

            var whereClientFilter = employeeService.GetEmployees(new EmployeeFilter()
            {
                BirthDayRange = new Tuple<DateTime, DateTime>(DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004")),
            }).Result;

            var orderByFilter = whereClientFilter.OrderBy(x => x.Id);

            var groupByFilter = whereClientFilter.GroupBy(x => x.BirthDate);

            var takeFilter = whereClientFilter.Take(5);

            //Assert

            Assert.Equal(takeFilter.Count(), 5);
        }

        [Fact]
        public async void Update_EmployeeTests()
        {
            //Arange

            var emploService = new EmployeeService(new BankDbContext());

            var employee = emploService.GetEmployees(new EmployeeFilter() { Id = Guid.Parse("c187a3a2-943c-e65e-4660-0a89e2b4cca6") })
                .Result.FirstOrDefault();

            var newEmployee = new Employee()
            {
                FirstName = "Jim"
            };

            //Act

            await emploService.Update(employee.Id, newEmployee);

            var updateEmployee = emploService.GetEmployees(new EmployeeFilter() { Id = employee.Id })
                .Result
                .FirstOrDefault(x => x.Id == employee.Id);

            //Assert
            Assert.Equal(employee.FirstName, updateEmployee.FirstName);
        }

        [Fact]
        public async void Delete_EmployeeTests()
        {
            //Arange

            var emploService = new EmployeeService(new BankDbContext());

            var employees = emploService.GetEmployees(new EmployeeFilter() { Passport = 1 }).Result;

            var employee = emploService.GetEmployees(new EmployeeFilter()
            {
                Passport = 1
            }).Result.FirstOrDefault();

            //Act
            await emploService.Delete(employee.Id);

            var deletedEmployee = emploService.GetEmployees(new EmployeeFilter() {Passport = 1}).Result;

            //Assert

            Assert.Equal(deletedEmployee.Count, employees.Count - 1);
        }
    }
}