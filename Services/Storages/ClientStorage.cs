using System;
using System.Linq;
using Services.Storage;
using Models.ModelsDb;
using Models;
using System.Collections.Generic;

namespace Services
{
    public class ClientStorage : IClientStorage
    {
        public Dictionary<Client, List<Account>> Data { get; }

        public ClientStorage()
        {
            Data = new Dictionary<Client, List<Account>>();
        }

        public void Add(Client client)
        {
            var accountList = new List<Account> { new Account()
            {
                CurrencyName = "USD",
                Amount = 0,
                ClientId = client.Id
            } };

            Data.Add(client, accountList);
        }

        public void Update(Client item)
        {
            var result = Data.FirstOrDefault(x => x.Key.Passport == item.Passport).Key;

            var listAccount = Data[result];

            Data.Remove(result);
            Data.Add(item, listAccount);
        }

        public void Delete(Client item)
        {
            Data.Remove(item);
        }

        public void AddAccount(Client item, Account account)
        {
            Data[item].Add(account);
        }

        public void UpdateAccount(Client item, Account account)
        {
            var result = Data.FirstOrDefault(x => x.Value
                .Find(x => x.ClientId == account.ClientId).ClientId == account.ClientId);

            result.Value[1] = account;
        }

        public void DeleteAccount(Client item, Account account)
        {
            Data[item].Remove(account);
        }
    }
}