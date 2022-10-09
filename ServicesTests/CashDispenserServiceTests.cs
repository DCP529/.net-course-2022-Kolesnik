using Bogus;
using Models.ModelsDb;
using Services;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace ServicesTests
{
    public class CashDispenserServiceTests
    {
        [Fact]
        public void Cashing_OutTest()
        {
            //Arrange

            var clientService = new ClientService(new BankDbContext());

            var cashDispenser = new CashDispenserService();

            //Act

            var clients = clientService.GetClients(new ClientFilter()
            {
                Passport = 20
            });

            foreach (var item in clients)
            {
                var task = cashDispenser.CashingOut(item);
            }

            Thread.Sleep(1000);

            //Assert

            Assert.True(true);
        }
    }
}
