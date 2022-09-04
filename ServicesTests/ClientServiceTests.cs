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
                BirthDate = DateTime.Parse("01.01.2004")
            };

            //Act/Assert

            Assert.Throws<PassportNullException>(() => clientService.AddClient(client));
        }
    }
}
