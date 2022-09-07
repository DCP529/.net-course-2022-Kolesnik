using Models;
using Services;
using Services.Exceptions;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ServicesTests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void Employee_throw_AgeLimitException()
        {
            //Arrange

            EmployeeService employeeService = new EmployeeService(new EmployeeStorage());

            var employee = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2005"),
                Passport = 2
            };

            //Act/Assert

            Assert.Throws<AgeLimitException>(() => employeeService.AddEmployee(employee));
        }

        [Fact]
        public void Employee_throw_PassportNullException()
        {
            //Arrange

            EmployeeService employeeService = new EmployeeService(new EmployeeStorage());

            var employee = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2004"),
                Passport = 0
            };

            //Act/Assert

            Assert.Throws<PassportNullException>(() => employeeService.AddEmployee(employee));
        }

        [Fact]
        public void Get_Employees_QueryTests()
        {
            //Arange

            EmployeeService employeeService = new EmployeeService(new EmployeeStorage());

            var employee1 = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
            };

            var employee2 = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.1987"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
            };

            var employee3 = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.1997"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
            };

            //Act

            employeeService.AddEmployee(employee1);
            employeeService.AddEmployee(employee2);
            employeeService.AddEmployee(employee3);


            var dataQuery = employeeService.GetEmployees(new EmployeeFilter()
            {
                BirthDayRange = new DateTime[2] { DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004") }
            });

            var young = dataQuery.Max(x => x.BirthDate);

            var old = dataQuery.Min(x => x.BirthDate);

            var averageAge = new DateTime((long)dataQuery.Average(x => x.BirthDate.Ticks));

            //Assert
            Assert.Equal(young, employee1.BirthDate);
            Assert.Equal(old, employee2.BirthDate);
            Assert.Equal(averageAge.Year, 1995);
        }
    }
}