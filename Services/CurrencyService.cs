using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CurrencyService
    {
        public async Task<decimal> ConvertCurrency(CurrencyApiRequest currencyRequest)
        {
            CurrencyApiResponse resultRequest;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync($"https://www.amdoren.com/api/currency.php?api_key={currencyRequest.ApiKey}&from={currencyRequest.FromCurrency}&to={currencyRequest.ToCurrency}&amount={currencyRequest.Amount}");

                responseMessage.EnsureSuccessStatusCode();

                string message = await responseMessage.Content.ReadAsStringAsync();

                resultRequest = JsonConvert.DeserializeObject<CurrencyApiResponse>(message);
            }

            return resultRequest.Amount;

        }
    }
}