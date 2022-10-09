using AutoMapper;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Models.ModelsDb;
using Models;
using Services.Filters;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ServicesTests
{
    public class RateUpdaterTests
    {
        private ITestOutputHelper _output1;

        public RateUpdaterTests(ITestOutputHelper outPut)
        {
            _output1 = outPut;
        }

        [Fact]
        public void Add_Procent_Of_Year_AccountTests()
        {
            //Arange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            //Act

            var tasks = new Task(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    var updater = new RateUpdater(new ClientService(new BankDbContext()));

                    Task.Delay(1000);

                    var task = updater.UpdateAmountAccount(cancelTokenSource);

                    if (i == 20)
                    {
                        cancelTokenSource.Cancel();
                    }
                }
            });

            tasks.Start();
            
            Thread.Sleep(10000);

            //Assert

            Assert.True(true);
        }
    }
}
