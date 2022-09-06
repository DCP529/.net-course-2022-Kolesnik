﻿using Models;
using Services;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ServicesTests
{
    public class EmployeeServiceTests
    {
        private Employee _employee = new Employee()
        {
            BirthDate = DateTime.Parse("01.01.2005"),
            Passport = 2,
            Patronymic = "Jane",
            Phone = 111,
            FirstName = "Marry",
            LastName = "Watson",
            Contract = "",
            Salary = 1500
        };

        [Fact]
        public void Employee_throw_AgeLimitExceptionTest() 
        {
            //Arrange

            EmployeeService employeeService = new EmployeeService();

            var employee = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2005"),
                Passport = 2
            };

            //Act/Assert

            Assert.Throws<AgeLimitException>(() => employeeService.AddEmployee(
                employee,
                new Account() { Amount = 10 },
                new Account() { Amount = 1 },
                new Account() { Amount = 20 }
                ));
        }

        [Fact]
        public void Employee_throw_PassportNullExceptionTest()
        {
            //Arrange

            EmployeeService employeeService = new EmployeeService();

            var employee = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2004")
            };

            //Act/Assert

            Assert.Throws<PassportNullException>(() => employeeService.AddEmployee(
                employee,
                new Account() { Amount = 10 },
                new Account() { Amount = 1 },
                new Account() { Amount = 20 }));
        }
    }
}
