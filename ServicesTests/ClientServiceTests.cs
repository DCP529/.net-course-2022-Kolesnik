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

            var clientService = new ClientService(new BankDbContext());

            var ts = new TestDataGenerator();


            //Act

            List<Client> clients = ts.GenerateListClient();

            foreach (var client in clients)
            {
                clientService.AddClient(client);
            }

            var whereClientFilter = clientService.GetClients(new ClientFilter()
            {
                BirthDayRange = new Tuple<DateTime, DateTime>(DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004")),
            });

            var orderByFilter = whereClientFilter.OrderBy(x => x.Phone);

            var groupByFilter = whereClientFilter.GroupBy(x => x.BirthDate);

            var takeFilter = whereClientFilter.Take(5);

            //Assert

            Assert.Equal(takeFilter.Count(), 5);
        }

        [Fact]
        public void Update_ClientTests()
        {
            //Arange

            var clientService = new ClientService(new BankDbContext());

            var client = new Client()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Петр",
                LastName = "Сидоров",
                Passport = 98,
                Patronymic = "Игоревич",
                Phone = 1
            };

            //Act

            clientService.AddClient(client);

            client.FirstName = "Станислав";

            clientService.Update(Guid.Parse("4c233c84-d0c6-4ff9-bb6b-dfbd517be79c"), client);

            var updateClient = clientService.GetClientById(Guid.Parse("4c233c84-d0c6-4ff9-bb6b-dfbd517be79c"));

            clientService.Update(Guid.Parse("4c233c84-d0c6-4ff9-bb6b-dfbd517be79c"), client);

            updateClient = clientService.GetClientById(Guid.Parse("4c233c84-d0c6-4ff9-bb6b-dfbd517be79c"));

            //Assert
            Assert.Equal(client.Passport, updateClient.Passport);
        }

        [Fact]
        public void Delete_ClientTests()
        {
            //Arange

            var clientService = new ClientService(new BankDbContext());

            var clients = clientService.GetClients(new ClientFilter()
            {
                Passport = 1
            });

            //Act
            
            clientService.Delete(Guid.Parse("04bcee72-6c75-419c-93ee-2e27634908e7"));

            var deletedClient = clientService.GetClients(new ClientFilter()
            {
                Passport = 1
            });

            //Assert
            Assert.Equal(deletedClient.Count, clients.Count - 1);
        }

        
    }
}

