using Models;
using Services;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ServicesTests
{
    public class ClientServiceTests
    {
        [Fact]
        public void Client_throw_AgeLimitExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService();

            var client = new Client()
            {
                BirthDate = DateTime.Parse("01.01.2005"),
                Passport = 2
            };

            //Act/Assert

            Assert.Throws<AgeLimitException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void Client_throw_PassportNullExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService();

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

            ClientService clientService = new ClientService();

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
