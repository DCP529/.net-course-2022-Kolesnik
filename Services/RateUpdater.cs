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
            return Task.Factory.StartNew(() =>
            {
                var clients = _clientService.GetClients(new ClientFilter() { Passport = 20 }).Result;

            while (!cancellation.IsCancellationRequested)
            {
                foreach (var item in clients)
                {
                    var account = item.Accounts.FirstOrDefault(x => x.CurrencyName == "USD");
                    account.Amount += Convert.ToInt32(account.Amount * 0.3);

                        var task = _clientService.UpdateAccount(item.Id, new Account()
                        {
                            Amount = account.Amount,
                            CurrencyName = account.CurrencyName,
                            ClientId = account.ClientId,
                            Id = account.Id
                        });
                    }
                }
            });
        }
    }
}
