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
        private Client _client = new Client()
        {
            BirthDate = DateTime.Parse("01.01.2005"),
            Passport = 2,
            Patronymic = "Hendrics",
            Phone = 111,
            FirstName = "Bob",
            LastName = "Carl"
        };

        [Fact]
        public void Client_throw_AgeLimitExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService();            

            //Act/Assert

            Assert.Throws<AgeLimitException>(() => clientService.AddClient
            (
                _client,
                new Account() { Amount = 15 },
                new Account() { Amount = 12 }
                )
            );
        }

        [Fact]
        public void Client_throw_PassportNullExceptionTest()
        {
            //Arrange

            ClientService clientService = new ClientService();

            _client.BirthDate = DateTime.Parse("01.01.2004");
            _client.Passport = 0;

            //Act/Assert

            Assert.Throws<PassportNullException>(() => clientService.AddClient
            (
                _client,
                new Account() { Amount = 10 },
                new Account() { Amount = 1 },
                new Account() { Amount = 20 }
                )
            );
        }
    }
}
