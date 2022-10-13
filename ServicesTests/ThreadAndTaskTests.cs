using AutoMapper;
using Bogus;
using ExportTool;
using Models;
using Models.ModelsDb;
using Services;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ServicesTests
{
    public class ThreadAndTaskTests
    {
        private ITestOutputHelper _output1;

        public ThreadAndTaskTests(ITestOutputHelper outPut)
        {
            _output1 = outPut;
        }


        [Fact]
        public void Add_100Dollars_AccountTests()
        {
            //Arange
            var clientService = new ClientService(new BankDbContext());

            var account = new Account() { Amount = 0, CurrencyName = "USD" };

            var locker = new object();

            //Act

            void AddMoney()
            {
                for (int i = 0; i < 10; i++)
                {
                    lock (locker)
                    {
                        account.Amount += 100;
                        Thread.Sleep(10);

                        _output1.WriteLine($"Поток {Thread.CurrentThread.Name} начислил 100 зелени, текущий счет = {account.Amount}");
                    }
                }
            }

            Thread thread2 = new Thread(AddMoney);
            thread2.Name = "thread2";

            thread2.Start();


            Thread thread1 = new Thread(AddMoney);
            thread1.Name = "thread1";

            thread1.Start();

            Thread.Sleep(1000);

            //Assert

            Assert.Equal(2000, account.Amount);
        }

        [Fact]
        public void Paralel_Import_And_ExportTests()
        {
            //Arange

            ClientService clientService;

            var export = new ExportService();

            var path = @"C:\\Users\\37377\\source\\repos\\.net-course-2022-Kolesnik\\ExportFiles";

            //Act

            var clientCsvList = export.DataExportToDatabase();


            Thread importThread = new(() =>
            {
                clientService = new ClientService(new BankDbContext());

                foreach (var item in clientCsvList)
                {
                    clientService.AddClient(item);

                    _output1.WriteLine($"Поток {Thread.CurrentThread.Name} добавляет клиента в бд из файла");

                }
            });

            importThread.Start();

            Thread exportThread = new(() =>
            {
                clientService = new ClientService(new BankDbContext());

                export.DataExportClientList(clientService.GetClients(new ClientFilter() { Passport = 5 }),
                    @"C:\Users\37377\source\repos\.net-course-2022-Kolesnik\ExportFiles\\ClientFromDatabase");

                _output1.WriteLine($"Поток {Thread.CurrentThread.Name} записывает клиента из бд в файл");
            });

            exportThread.Start();

            Thread.Sleep(10000);

            //Assert

            Assert.True(true);
        }
    }
}
