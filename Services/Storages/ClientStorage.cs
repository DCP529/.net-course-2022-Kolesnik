using System;
using System.Linq;
using Services.Storage;
using Models.ModelsDb;

namespace Services
{
    public class ClientStorage : IClientStorage
    {
        public BankDbContext Data { get; }

        public ClientStorage()
        {
            Data = new BankDbContext();
        }

        public ClientDb GetClientById(Guid clientId)
        {
            return Data.Clients.FirstOrDefault(x => x.Id == clientId);
        }

        public void Add(ClientDb client)
        {
            var defaultAccount = new AccountDb()
            {
                CurrencyName = "USD",
                Amount = 0,
                ClientId = client.Id
            };

            Data.Clients.Add(client);
            Data.Accounts.Add(defaultAccount);

            Data.SaveChanges();
        }

        public void Update(Guid clientId, ClientDb client) 
        {
            var getClient = Data.Clients.FirstOrDefault(x => x.Id == client.Id);

            Data.Entry(getClient).CurrentValues.SetValues(client);

            Data.SaveChanges();
        }

        public void Delete(Guid clientId)
        {
            Data.Clients.Remove(Data.Clients.FirstOrDefault(x => x.Id == clientId));

            Data.SaveChanges();
        }

        public void AddAccount(Guid clientId, AccountDb account)
        {
            account.ClientId = clientId;

            Data.Accounts.Add(account);

            Data.SaveChanges();
        }

        public void UpdateAccount(Guid clientId, AccountDb account)
        {
            var getAccount = Data.Accounts.FirstOrDefault(x => x.ClientId == clientId);

            Data.Entry(getAccount).CurrentValues.SetValues(account);

            Data.SaveChanges();
        }

        public void DeleteAccount(Guid clientId, AccountDb account)
        {
            var deleteAccount = Data.Accounts.Where(x => x.ClientId == clientId);

            if (deleteAccount.Contains(account))
            {
                Data.Accounts.Remove(account);
            }

            Data.SaveChanges();
        }
    }
}
