using Models;
using Services;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.ModelsDb;
using Xunit;
using Services.Filters;

namespace ServicesTests
{
    public class ClientServiceTests
    {

        [Fact]
        public void Filter_ClientsTest()
        {
            //Arange

            var clientService = new ClientService(new ClientStorage());

            var ts = new TestDataGenerator();


            //Act

            List<ClientDb> clients = ts.GenerateListClient();

            foreach (var client in clients)
            {
                clientService.AddClient(client);
            }

            var whereClientFilter = clientService.GetClientsList(new ClientFilter()
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
        public void Update_ClientTests()
        {
            //Arange

            var clientService = new ClientService(new ClientStorage());

            var client1 = new ClientDb()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Id = Guid.NewGuid()
            };

            //Act

            clientService.AddClient(client1);

            client1.FirstName = "Станислав";

            clientService.Update(client1);

            var updateClient = clientService.GetClientById(client1.Id);

            clientService.Update(client1);

            var updateClient = clientService.GetClientById(client1.Id);

            //Assert
            Assert.Equal(client1.Passport, updateClient.Passport);
        }

        [Fact]
        public void Delete_ClientTests()
        {
            //Arange

            var clientService = new ClientService(new ClientStorage());



            var client = new ClientDb()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Id = Guid.NewGuid()
            };

            //Act

            //clientService.AddClient(client);
            //clientService.Delete(client);

            var deletedClient = clientService.GetClientsList(new ClientFilter()
            {
                Passport = 1
            });

            //Assert
            Assert.Equal(deletedClient.Count, 9);
        }

        
    }
}

