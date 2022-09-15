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
        public void Client_throw_AgeLimitExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService(new ClientStorage());

            var client = new ClientDb()
            {
                BirthDate = DateTime.Parse("01.01.2005"),
                Passport = 2,
                Id = Guid.NewGuid()
            };

            //Act/Assert

            Assert.Throws<AgeLimitException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void Client_throw_PassportNullExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService(new ClientStorage());

            var client = new ClientDb()
            {
                BirthDate = DateTime.Parse("01.01.2004"),
                Passport = 0,
                Id = Guid.NewGuid()
            };

            //Act/Assert

            Assert.Throws<PassportNullException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void Add_2_Idential_ClientExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService(new ClientStorage());

            var client1 = new ClientDb()
            {
                BirthDate = DateTime.Parse("01.01.2004"),
                Passport = 1,
                FirstName = "",
                LastName = "",
                Patronymic = "",
                Phone = 77838196,
                Id = Guid.NewGuid()
            };

            //Act

            clientService.AddClient(client1);

            //Assert

            Assert.Throws<ArgumentException>(() => clientService.AddClient(client1));
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
                FirstName = "Станислав",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 77838196,
                Id = client.Id
            });

            //Assert
            Assert.Equal(deletedClient.Count, 0);
        }

        [Fact]
        public void Get_Clients_QueryTests()
        {
            //Arange

            ClientService clientService = new ClientService(new ClientStorage());

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

            var client2 = new ClientDb()
            {
                BirthDate = DateTime.Parse("01.01.1987"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 2,
                Patronymic = "Игоревич",
                Phone = 1,
                Id = Guid.NewGuid()
            };

            var client3 = new ClientDb()
            {
                BirthDate = DateTime.Parse("01.01.1997"),
                FirstName = "Сережа",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
                Id = Guid.NewGuid()
            };

            //Act

            clientService.AddClient(client1);
            clientService.AddClient(client2);
            clientService.AddClient(client3);
            

            var clientDictionary = clientService.GetClientsList(new ClientFilter()
            {
                BirthDayRange = new Tuple<DateTime, DateTime>(DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004"))
            });

            var young = clientDictionary.Max(x => x.BirthDate);

            var old = clientDictionary.Min(x => x.BirthDate);

            var averageAge = new DateTime((long)clientDictionary.Average(x => x.BirthDate.Ticks));

            var clientFIOAndBirthDate = clientService.GetClientsList(new ClientFilter()
            {
                FirstName = "Сергей",
                LastName = "Сидоров",
                Patronymic = "Игоревич",
                Passport = 2,
                BirthDayRange = new Tuple<DateTime, DateTime>(DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004"))
            });

            //Assert
            Assert.Equal(young, client1.BirthDate);
            Assert.Equal(old, client2.BirthDate);
            Assert.Equal(averageAge.Year, 1995);
        }

        [Fact]
        public void Add_Client_AccountTests()
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

            clientService.AddClient(client);
            clientService.AddAccount(client, new AccountDb());

            var result = clientService.GetClientsList(new ClientFilter(){
                BirthDayRange = new Tuple<DateTime, DateTime>(DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004")),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
            });

            //Assert
            Assert.Equal(result.Count, 2);
        }

        [Fact]
        public void Update_Client_AccountTests()
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

            var account1 = new AccountDb()
            {
                CurrencyName = "UA",
                Amount = 1_500
            };

            var account2 = new AccountDb()
            {
                CurrencyName = "UA",
                Amount = 20_000//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            };

            //Act

            clientService.AddClient(client);
            clientService.AddAccount(client, account1);
            clientService.UpdateAccount(client, account2);

            var getClient = clientService.GetClientsList(new ClientFilter() { Passport = client.Passport })
                .FirstOrDefault(x => x.Passport == client.Passport);

            //var updateAccount = getClient.Value.Find(x => x.Currency.Code == account2.Currency.Code);

            ////Assert

            //Assert.Equal(account2.Currency.Code, updateAccount.Currency.Code);
        }

        [Fact]
        public void Delete_Client_AccountTests()
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

            var account = new AccountDb()
            {
                CurrencyName = "UA",
                Amount = 1_500
            };

            //Act

            clientService.AddClient(client);
            clientService.AddAccount(client, account);
            clientService.DeleteAccount(client, account);

            var getClient = clientService.GetClientsList(new ClientFilter() { Passport = client.Passport })
                .FirstOrDefault(x => x.Passport == client.Passport);

            //Assert

            //Assert.Equal(getClient.Value.Count, 1);////////////////////////////////////////////////////////////////////////////
        }
    }
}

