using Models;
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
        [Fact]
        public void Employee_throw_AgeLimitException()
        {
            //Arrange

            EmployeeService employeeService = new EmployeeService();

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

            EmployeeService employeeService = new EmployeeService();

            var employee = new Employee()
            {
                BirthDate = DateTime.Parse("01.01.2004"),
                Passport = 0
            };

            //Act/Assert

            Assert.Throws<PassportNullException>(() => employeeService.AddEmployee(employee));
        }
    }
}