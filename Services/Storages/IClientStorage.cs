using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Models.ModelsDb;

namespace Services.Storage
{
    public interface IClientStorage : IStorage<ClientDb>
    {
        public BankDbContext Data { get; }

        public void AddAccount(Guid clientId, AccountDb account);

        public void UpdateAccount(Guid clientId, AccountDb account);

        public void DeleteAccount(Guid clientId, AccountDb account);

        public ClientDb GetClientById(Guid clientId);
    }
}
