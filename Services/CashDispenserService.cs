using Bogus;
using Models;
using Models.ModelsDb;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Services
{
    public class CashDispenserService
    {
        public Task CashingOut(Client client)
        {

            var clientService = new ClientService(new BankDbContext());

            var account = client.Accounts.FirstOrDefault(x => x.CurrencyName == "USD"));

            if (account.Amount >= 100)
            {
                account.Amount -= 100;
            }

            clientService.UpdateAccount(client.Id, new Account()
            {
                Amount = account.Amount,
                CurrencyName = account.CurrencyName,
                ClientId = account.ClientId,
                Id = account.Id
            }).Wait();
        }
    }
}
