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

        public Task UpdateAmountAccount(CancellationTokenSource cancellation)
        {
            return Task.Factory.StartNew(() =>
            {
                var clients = _clientService.GetClients(new ClientFilter() { Passport = 20 });

                foreach (var item in clients)
                {
                    Task.Delay(1000);

                    var account = item.Accounts.FirstOrDefault(x => x.CurrencyName == "USD");
                    account.Amount += Convert.ToInt32(account.Amount * 0.3);

                    _clientService.UpdateAccount(item.Id, new Account()
                    {
                        Amount = account.Amount,
                        CurrencyName = account.CurrencyName,
                        ClientId = account.ClientId,
                        Id = account.Id
                    });
                }
            });
        }
    }
}
