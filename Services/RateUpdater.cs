using AutoMapper;
using Models;
using Models.ModelsDb;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class RateUpdater
    {
        private ClientService _clientService { get; set; }

        public RateUpdater(ClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task UpdateAmountAccount(CancellationTokenSource cancellation)
        {
            var clients = await _clientService.GetClientsAsync(new ClientFilter() { Passport = 20 });

            while (!cancellation.IsCancellationRequested)
            {
                foreach (var item in clients)
                {
                    var account = item.Accounts.FirstOrDefault(x => x.CurrencyName == "USD");
                    account.Amount += Convert.ToInt32(account.Amount * 0.3);

                    await _clientService.UpdateAccountAsync(item.Id, new Account()
                    {
                        Amount = account.Amount,
                        CurrencyName = account.CurrencyName,
                        ClientId = account.ClientId,
                        Id = account.Id
                    });
                }
            }
        }
    }
}
