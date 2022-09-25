using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Services.Storage;
using Services.Filters;
using Models;
using Models.ModelsDb;

namespace Services
{
    public class ClientService
    {
        private IClientStorage _clients;

        public ClientService(IClientStorage clientStorage)
        {
            _clients = clientStorage;
        }
        public void AddClient(ClientDb client)
        {
            if (client.BirthDate > DateTime.Parse("31.12.2004"))
            {
                throw new AgeLimitException("Возраст клиента должен быть больше 18!");
            }

            if (client.Passport == 0)
            {
                throw new PassportNullException("Нельзя добавить клиента без паспортных данных!");
            }

            
            _clients.Add(client);
            
        }

        public ClientDb GetClientById(Guid clientId) 
        {
            IsClientInDatabase(_clients.Data.Clients.FirstOrDefault(x => x.Id == clientId));

            return _clients.GetClientById(clientId);
        }

        public List<ClientDb> GetClientsList(ClientFilter clientFilters)
        {
            var query = _clients.Data.Clients.AsQueryable();

            if (clientFilters.FirstName != null && clientFilters.LastName != null && clientFilters.Patronymic != null)
            {
                query = query.Where(x => x.FirstName == clientFilters.FirstName)
                    .Where(x => x.LastName == clientFilters.LastName)
                    .Where(x => x.Patronymic == clientFilters.Patronymic);
            }

            if (clientFilters.Passport != 0)
            {
                query = query.Where(x => x.Passport == clientFilters.Passport);
            }

            if (clientFilters.Phone != 0)
            {
                query = query.Where(x => x.Phone == clientFilters.Phone);
            }

            if (clientFilters.BirthDayRange != null)
            {
                query = query.Where(x => x.BirthDate >= clientFilters.BirthDayRange.Item1 &&
                                         x.BirthDate <= clientFilters.BirthDayRange.Item2);
            }

            if (clientFilters.Id != Guid.Empty)
            {
                query = query.Where(x => x.Id == clientFilters.Id);
            }

            
            return query.ToList();
        }

        public void Update(ClientDb client)
        {
            IsClientInDatabase(client);

            _clients.Update(client.Id, client);
        }

        public void Delete(ClientDb client)
        {
            IsClientInDatabase(client);

            _clients.Delete(client.Id);
        }

        public void AddAccount(ClientDb client, AccountDb account)
        {
            IsClientInDatabase(client);

            _clients.AddAccount(client.Id, account);
        }

        public void UpdateAccount(ClientDb client, AccountDb account) 
        {
            IsAccountInDatabase(account);

            _clients.UpdateAccount(client.Id, account);
        }

        public void DeleteAccount(ClientDb client, AccountDb account)
        {
            IsAccountInDatabase(account);

            _clients.DeleteAccount(client.Id, account);
        }

        private void IsClientInDatabase(ClientDb client)
        {
            if (!_clients.Data.Clients.Contains(_clients.Data.Clients.FirstOrDefault(x => x.Id == client.Id)))
            {
                throw new ArgumentException("Клиент не найден!");
            }
        }

        private void IsAccountInDatabase(AccountDb account)
        {
            if (!_clients.Data.Accounts.Contains(_clients.Data.Accounts.FirstOrDefault(x => x.Id == account.Id)))
            {
                throw new ArgumentException("Аккаунт не найден!");
            }
        }
    }
}
