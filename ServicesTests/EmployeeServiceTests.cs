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


            var employeesDictionary = employeeService.GetEmployees(new EmployeeFilter()
            {
                BirthDayRange = new Tuple<DateTime, DateTime>(DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004"))
            });

            var young = employeesDictionary.Max(x => x.BirthDate);

            var old = employeesDictionary.Min(x => x.BirthDate);

            var averageAge = new DateTime((long)employeesDictionary.Average(x => x.BirthDate.Ticks));

            //Assert
            Assert.Equal(young, employee1.BirthDate);
            Assert.Equal(old, employee2.BirthDate);
            Assert.Equal(averageAge.Year, 1995);
        }

        [Fact]
        public void Add_EmployeeTests()
        {
            //Arrange

            var emploService = new EmployeeService(new EmployeeStorage());

            var employee1 = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Contract = "",
                Salary = 1_500
            };

            //Act

            emploService.AddEmployee(employee1);

            var listEmployee = emploService.GetEmployees(new EmployeeFilter() {Passport = 1});

            //Assert

            Assert.Equal(listEmployee.Count, 1);
        }

        [Fact]
        public void Update_EmployeeTests()
        {
            //Arange

            var emploService = new EmployeeService(new EmployeeStorage());

            var employee1 = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Contract = "",
                Salary = 1_500
            };

            var employee2 = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Евгений",
                LastName = "Зеленский",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Contract = "",
                Salary = 15_000
            };

            //Act

            emploService.AddEmployee(employee1);
            emploService.Update(employee2);

            var updateEmployee = emploService.GetEmployees(new EmployeeFilter() { Passport = employee1.Passport })
                .FirstOrDefault(x => x.Passport == employee1.Passport);

            //Assert
            Assert.Equal(employee1.Passport, updateEmployee.Passport);
        }

        [Fact]
        public void Delete_EmployeeTests()
        {
            //Arange

            var emploService = new EmployeeService(new EmployeeStorage());

            var employee1 = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Contract = "",
                Salary = 1_500
            };

            //Act

            emploService.AddEmployee(employee1);
            emploService.Delete(employee1);

            var deletedEmployee = emploService.GetEmployees(new EmployeeFilter() {Passport = 1});

            //Assert

            Assert.Equal(deletedEmployee.Count, 0);
        }
    }
}