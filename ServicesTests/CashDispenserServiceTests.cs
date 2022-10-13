using Models.ModelsDb;
using Services;
using Services.Filters;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ServicesTests
{
    public class CashDispenserServiceTests
    {
        private ITestOutputHelper _output;

        public CashDispenserServiceTests(ITestOutputHelper outPut)
        {
            _output = outPut;
        }

        [Fact]
        public void Cashing_OutTest()
        {
            //Arrange            

            ThreadPool.SetMaxThreads(20, 20);
            ThreadPool.GetAvailableThreads(out var worker, out var completition);

            var clientService = new ClientService(new BankDbContext());

            var cashDispenser = new CashDispenserService();

            //Act

            var clients = clientService.GetClientsAsync(new ClientFilter()
            {
                Passport = 20
            });

            var clients1 = clients.Result.Take(22);

            foreach (var item in clients1)
            {
                var task = cashDispenser.CashingOutAsync(item);

                _output.WriteLine($"Запущен поток  с  id {task.Id}, свободных потоков {worker}");
            }

            Task.Delay(2000).Wait();

            //Assert

            Assert.True(true);
        }
    }
}
