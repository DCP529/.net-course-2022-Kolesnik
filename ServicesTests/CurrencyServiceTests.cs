using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests
{
    public class CurrencyServiceTests
    {
        [Fact]
        public async void Currency_ConvertTest()
        {
            //Arange

            var currencyService = new CurrencyService();

            var currencyRequest = new CurrencyApiRequest() 
            {
                Amount = 100,
                FromCurrency = "USD",
                ToCurrency = "RUB",
                ApiKey = "DeGteKn6cectyYwXvGPdCEvkW9fL8s"
            };

            //Act

            var convertableAmount = await currencyService.ConvertCurrency(currencyRequest);

            //Assert

            Assert.True(convertableAmount != 100);
        }

    }
}
