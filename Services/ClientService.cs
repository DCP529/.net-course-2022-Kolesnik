using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services.Storage;
using Services.Filters;

namespace Services
{
    public class ClientService
    {
        private IClientStorage _clients = new ClientStorage();

        public ClientService(IClientStorage clientStorage)
        {
            _clients = clientStorage;
        }
        public void AddClient(Client client)
        {
            if (client.BirthDate > DateTime.Parse("31.12.2004"))
            {
                throw new AgeLimitException("Возраст клиента должен быть больше 18!");
            }

            if (client.Passport == 0)
            {
                throw new PassportNullException("Нельзя добавить клиента без паспортных данных!");
            }

            var accountList = new List<Account> { new Account() { Currency = new Currency() { Code = 1, Name = "USD" } } };

            _clients.Add(client);
        }

        public Dictionary<Client, List<Account>> GetClients(ClientFilter clientFilters)
        {
            IEnumerable<KeyValuePair<Client, List<Account>>> query = _clients.Data.Select(t => t);

            if (clientFilters.FirstName != null && clientFilters.LastName != null && clientFilters.Patronymic != null)
            {
                query.Where(x => x.Key.FirstName == clientFilters.FirstName)
                    .Where(x => x.Key.LastName == clientFilters.LastName)
                    .Where(x => x.Key.Patronymic == clientFilters.Patronymic);
            }

            if (clientFilters.Passport != 0)
            {
                query = query.Where(x => x.Key.Passport == clientFilters.Passport);
            }

            if (clientFilters.Phone != 0)
            {
                query.Where(x => x.Key.Phone == clientFilters.Phone);
            }

            if (clientFilters.BirthDayRange != null)
            {
                query.Where(x => x.Key.BirthDate >= clientFilters.BirthDayRange.Item1 &&
                                 x.Key.BirthDate <= clientFilters.BirthDayRange.Item2);
            }

            return query.ToDictionary(t => t.Key, t => t.Value);
        }

        public void Update(Client client)
        {
            _clients.Update(client);
        }

        public void Delete(Client client)
        {
            _clients.Delete(client);
        }

        public void AddAccount(Client client, Account account)
        {
            _clients.AddAccount(client, account);
        }

        public void UpdateAccount(Client client, Account account)
        {
            _clients.UpdateAccount(client, account);
        }

        public void DeleteAccount(Client client, Account account)
        {
            _clients.DeleteAccount(client, account);
        }
    }
}
