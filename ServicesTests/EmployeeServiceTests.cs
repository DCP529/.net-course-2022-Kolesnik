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
        public void Filter_ClientsTest()
        {
            //Arange

            var employeeService = new EmployeeService(new EmployeeStorage());

            var ts = new TestDataGenerator();


            //Act

            List<EmployeeDb> employees = ts.GenerateListEmployee();

            foreach (var employee in employees)
            {
                employeeService.AddEmployee(employee);
            }

            var whereClientFilter = employeeService.GetEmployees(new EmployeeFilter()
            {
                BirthDayRange = new Tuple<DateTime, DateTime>(DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004")),
            });

            var orderByFilter = whereClientFilter.OrderBy(x => x.Id);

            var groupByFilter = whereClientFilter.GroupBy(x => x.BirthDate);

            var takeFilter = whereClientFilter.Take(5);

            //Assert

            Assert.Equal(takeFilter.Count(), 5);
        }

        [Fact]
        public void Update_EmployeeTests()
        {
            //Arange

            var emploService = new EmployeeService(new EmployeeStorage());

            var employee1 = new EmployeeDb()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Contract = "",
                Salary = 1_500,
                Id = Guid.NewGuid()
            };

            //Act

            emploService.AddEmployee(employee1);

            employee1.FirstName = "Святополк";
            emploService.Update(employee1);

            var updateEmployee = emploService.GetEmployees(new EmployeeFilter() { Id = employee1.Id })
                .FirstOrDefault(x => x.Id == employee1.Id);

            //Assert
            Assert.Equal(employee1.Passport, updateEmployee.Passport);
        }

        [Fact]
        public void Delete_EmployeeTests()
        {
            //Arange

            var emploService = new EmployeeService(new EmployeeStorage());

            var employee1 = new EmployeeDb()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Contract = "",
                Salary = 1_500,
                Id = Guid.NewGuid()
            };

            //Act

            emploService.AddEmployee(employee1);
            emploService.Delete(employee1);

            var deletedEmployee = emploService.GetEmployees(new EmployeeFilter() {Passport = 1});

            //Assert

            Assert.Equal(deletedEmployee.Count, 6);
        }
    }
}