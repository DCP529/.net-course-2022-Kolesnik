using ExportTool;
using Models;
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
        public void Data_Export_To_FileTests()
        {
            //Arrange
            var client = new Client() { FirstName = "Gin", Passport = 20 };
            var employee = new Employee() { FirstName = "Bob", Passport = 20 };

            var exportData = new ExportService();

            //Act/Assert
            exportData.DataExportToFile(client);
            exportData.DataExportToFile(employee);
        }

        [Fact]
        public void Data_Export_ClientList_FileTests()
        {
            //Arrange
            var client = new List<Client>() { new Client() { FirstName = "Mark" , Passport = 20}, new Client() { FirstName = "Oleg" , Passport = 20 } };

            var exportData = new ExportService();

            //Act/Assert
            exportData.DataExportClientList(client);
        }

        [Fact]
        public void Data_Export_To_DatabaseTests()
        {
            //Arrange
            var exportData = new ExportService();

            //Act/Assert
            exportData.DataExportToDatabase();
        }
    }
}
