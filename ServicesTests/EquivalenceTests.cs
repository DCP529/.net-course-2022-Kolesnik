using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Models.ModelsDb;
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
            employeeList.Add(new Employee() { Phone = 77700001 });

            //Act

            var employee = new Employee() { Phone = 77700001 };

            var findEmployee = employeeList.Find(e => e == employee);

            //Asert
            Assert.NotNull(findEmployee);
        }

    }
}
