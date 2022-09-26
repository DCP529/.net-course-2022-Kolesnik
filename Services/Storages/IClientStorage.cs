using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Models.ModelsDb;

namespace Services.Storage
{
    public interface IClientStorage : IStorage<Client>
    {
        public Dictionary<Client, List<Account>> DataDictionary { get; }

        public void AddAccount(Client item, Account account);

        public void UpdateAccount(Client item, Account account);

        public void DeleteAccount(Client item, Account account);
    }
}
