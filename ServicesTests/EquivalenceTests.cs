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
            var client = dictionary.FirstOrDefault(c => c.Key.Phone == 77500001);
            var account = dictionary[client.Key];

            //Asert
            Assert.Equal(account, client.Value);
        }

        [Fact]
        public void ListEmployeeTest()
        {
            //Arange
            var dataGenerator = new TestDataGenerator();
            var employeeList = dataGenerator.GenerateListEmployee();

            //Act
            var employee1 = employeeList.FirstOrDefault(c => c.Phone == 77500001);
            var employee2 = employeeList[employee1.Phone];

            //Asert
            Assert.Equal(employee1, employee2);
        }

    }
}
