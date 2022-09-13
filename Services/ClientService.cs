using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Services.Storage;
using Services.Filters;

namespace Services
{
    public class ClientService
    {
        private IClientStorage _clients;

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

            if (!_clients.Data.ContainsKey(client))
            {
                _clients.Add(client);
            }
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
            IsClientInDictionary(client);

            _clients.Update(client);
        }

        public void Delete(Client client)
        {
            IsClientInDictionary(client);

            _clients.Delete(client);
        }

        public void AddAccount(Client client, Account account)
        {
            IsClientInDictionary(client);

            if (!_clients.Data[client].Contains(account))
            {
                _clients.AddAccount(client, account);
            }
        }

        public void UpdateAccount(Client client, Account account) 
        {
            IsClientInDictionary(client);

            IsAccountInDictionary(client, account);

            _clients.UpdateAccount(client, account);
        }

        public void DeleteAccount(Client client, Account account)
        {
            IsClientInDictionary(client);

            IsAccountInDictionary(client, account);

            _clients.DeleteAccount(client, account);
        }

        private void IsClientInDictionary(Client client)
        {
            var findClient = _clients.Data.FirstOrDefault(x => x.Key.Passport == client.Passport).Key;

            if (!_clients.Data.ContainsKey(findClient))
            {
                throw new ArgumentException("Клиент не найден!");
            }
        }

        private void IsAccountInDictionary(Client client, Account account)
        {
            var findAccount = _clients.Data[client].FirstOrDefault(x => x.Currency.Code == account.Currency.Code);

            if (!_clients.Data[client].Contains(findAccount))
            {
                throw new ArgumentException("Аккаунт не найден!");
            }
        }
    }
}
