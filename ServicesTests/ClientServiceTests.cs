using Models;
using Services;
using Services.Exceptions;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace ServicesTests
{
    public class ClientServiceTests
    {
        [Fact]
        public void Get_Clients_QueryTests()
        {
            //Arange

            ClientService clientService = new ClientService(new ClientStorage());

            var client1 = new Client()
            {
                BirthDate = DateTime.Parse("01.01.2003"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
            };

            var client2 = new Client()
            {
                BirthDate = DateTime.Parse("01.01.1987"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
            };

            var client3 = new Client()
            {
                BirthDate = DateTime.Parse("01.01.1997"),
                FirstName = "Сергей",
                LastName = "Сидоров",
                Passport = 1,
                Patronymic = "Игоревич",
                Phone = 1,
            };

            //Act

            clientService.AddClient(client1);
            clientService.AddClient(client2);
            clientService.AddClient(client3);


            var dataQuery = clientService.GetClients(new ClientFilter()
            {
                BirthDayRange = new DateTime[2] { DateTime.Parse("01.01.1922"), DateTime.Parse("31.12.2004") }
            });

            var young = dataQuery.Max(x => x.Key.BirthDate);

            var old = dataQuery.Min(x => x.Key.BirthDate);

            var averageAge = new DateTime((long)dataQuery.Average(x => x.Key.BirthDate.Ticks));

            //Assert
            Assert.Equal(young, client1.BirthDate);
            Assert.Equal(old, client2.BirthDate);
            Assert.Equal(averageAge.Year, 1995);
        }

        [Fact]
        public void Client_throw_AgeLimitExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService(new ClientStorage());

            var client = new Client()
            {
                BirthDate = DateTime.Parse("01.01.205"),
                Passport = 2
            };

            //Act/Assert

            Assert.Throws<AgeLimitException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void Client_throw_PassportNullExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService(new ClientStorage());

            var client = new Client()
            {
                BirthDate = DateTime.Parse("01.01.2004"),
                Passport = 0
            };

            //Act/Assert

            Assert.Throws<PassportNullException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void Add_2_Idential_ClientExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService(new ClientStorage());

            var client1 = new Client()
            {
                BirthDate = DateTime.Parse("01.01.2004"),
                Passport = 1,
                FirstName = "",
                LastName = "",
                Patronymic = "",
                Phone = 77838196
            };

            //Act

            clientService.AddClient(client1);

            //Assert

            Assert.Throws<ArgumentException>(() => clientService.AddClient(client1));
        }
    }
}
