using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Services.Storage;

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
                Currency = new Currency()
                {
                    Code = 1,
                    Name = "USD"
                }
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
                .Find(x => x.Currency.Code == account.Currency.Code).Currency.Code == account.Currency.Code);

            result.Value[1] = account;
        }

        public void DeleteAccount(Client item, Account account)
        {
            Data[item].Remove(account);
        }
    }
}
