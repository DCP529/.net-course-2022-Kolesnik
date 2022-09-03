using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ServicesTests
{
    public class EquivalenceTests
    {
        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            //Arange
            var dataGenerator = new TestDataGenerator();
            var dictionary = dataGenerator.GenerateDictionaryClientAccount();

            //Act
            var client1 = dictionary.FirstOrDefault(c => c.Key.Phone == 77500001);

            var client2 = new Client()
            {
                Phone = 77500001,
                BirthDate = client1.Key.BirthDate,
                FirstName = client1.Key.FirstName,
                LastName = client1.Key.LastName,
                Passport = client1.Key.Passport,
                Patronymic = client1.Key.Patronymic
            };

            var account = dictionary[client2];

            //Asert
            Assert.Equal(account, client1.Value);
        }

        [Fact]
        public void EmployeeListGetHashCodeNecessityPositivTest()
        {
            //Arange
            var dataGenerator = new TestDataGenerator();
            var employeeList = dataGenerator.GenerateListEmployee();

            //Act
            var employee1 = employeeList.FirstOrDefault(c => c.Phone == 77500001);
            var employee2 = employeeList.Find(x => x == employee1);

            var employee = new Employee()
            {
                BirthDate = employee2.BirthDate,
                FirstName = employee2.FirstName,
                LastName = employee2.LastName,
                Salary = employee2.Salary,
                Passport = employee2.Passport,
                Patronymic = employee2.Patronymic,
                Phone = employee2.Phone,
                Contract = employee2.Contract
            };

            //Asert
            Assert.Equal(employee, employee2);
        }

    }
}
