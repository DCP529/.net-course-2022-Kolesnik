using ExportTool;
using Models;
using Models.ModelsDb;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests
{
    public class ExportToolTests
    {
        [Fact]
        public async void Data_Export_To_FileTests()
        {
            //Arrange
            var client = new Client() { FirstName = "Gin", Passport = 20 };
            var employee = new Employee() { FirstName = "Bob", Passport = 20 };

            var exportData = new ExportService();

            //Act/Assert
            await exportData.DataExportToFileAsync(client, @"C:\Users\37377\source\repos\.net-course-2022-Kolesnik\ExportFiles\\Client");
            await exportData.DataExportToFileAsync(employee, @"C:\Users\37377\source\repos\.net-course-2022-Kolesnik\ExportFiles\\Employee");
        }

        [Fact]
        public async void Data_Export_ClientList_FileTests()
        {
            //Arrange
            var client = new List<Client>() { new Client() { FirstName = "Mark", Passport = 20 }, new Client() { FirstName = "Oleg", Passport = 20 } };

            var exportData = new ExportService();

            //Act/Assert
            await exportData.DataExportClientListAsync(client,
                @"C:\Users\37377\source\repos\.net-course-2022-Kolesnik\ExportFiles\\ClientList");
        }

        [Fact]
        public void Data_Export_To_DatabaseTests()
        {
            //Arrange
            var exportData = new ExportService();

            var clientService = new ClientService(new BankDbContext());

            //Act/Assert
            var clientListCsv = exportData.DataExportToDatabaseAsync();

            foreach (var item in clientListCsv.Result)
            {
                clientService.AddClientAsync(item);
            }
        }

        [Fact]
        public async void Serialize_DeserializeTests()
        {
            //Arange

            ExportService exportService = new ExportService();

            //Act

            await exportService.SerializableExportToFileAsync(new Client()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Passport = 22,
                Accounts = new List<Account>() 
                { 
                    new Account() 
                    {
                        CurrencyName = "USD"
                    }, 
                    new Account() 
                    { 
                        CurrencyName = "RUB"
                    } 
                }
            },
            @"C:\Users\37377\source\repos\.net-course-2022-Kolesnik\ExportFiles\\JsonClientList");


            var client = await exportService.DeserializableImportFromFile<Client>(@"C:\Users\37377\source\repos\.net-course-2022-Kolesnik\ExportFiles\\JsonClientList");
            //Assert

            Assert.Equal(typeof(Client), client.GetType());
        }
    }
}
