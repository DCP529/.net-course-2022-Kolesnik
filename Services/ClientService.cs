using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Storage;
using Services.Filters;
using Models;
using Models.ModelsDb;
using Bogus.DataSets;

namespace Services
{
    public class ClientService
    {
        private BankDbContext _clients;

        public ClientService(BankDbContext clientStorage)
        {
            _clients = clientStorage;
        }

        public Client GetClientById(Guid clientId) 
        {
            IsClientInDatabase(_clients.Clients.FirstOrDefault(x => x.Id == clientId));

            var clientConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDb, Client>());
            var clientMapper = new Mapper(clientConfig);

            return clientMapper.Map<Client>(_clients.Clients.FirstOrDefault(x => x.Id == clientId));
        }

        public List<Client> GetClients(ClientFilter clientFilters)
        {
            var list = new List<Client>();

            var query = _clients.Clients.Select(t => t);

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

            query.ToList();

            var clientConfig = new MapperConfiguration(cfg =>  cfg.CreateMap<ClientDb, Client>());

            var clientMapper = new Mapper(clientConfig);

            var accountConfig = new MapperConfiguration(cfg => cfg.CreateMap<AccountDb, Account>());

            var accountMapper = new Mapper(accountConfig);

            list.AddRange(clientMapper.Map<List<Client>>(query));

            foreach (var item in list)
            {
                var accounts = _clients.Accounts.Where(x => x.ClientId == item.Id).ToList();

                item.Accounts.AddRange(accountMapper.Map<List<Account>>(accounts));
            }

            return list;
        }

        public void Update(Guid id, Client client)
        {
            var clientConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDb>()
                .ForMember(x => x.Accounts, f => new AccountDb()
                {
                    CurrencyName = "USD",
                    Amount = 0,
                    ClientId = id
                }));

            var clientMapper = new Mapper(clientConfig);

            var clientDb = clientMapper.Map<ClientDb>(client);
            clientDb.Id = id;

            if (_clients.Clients.FirstOrDefault(x => x.Id == id) != null)
            {
                var getClient = _clients.Clients.FirstOrDefault(x => x.Id == id);

                _clients.Entry(getClient).CurrentValues.SetValues(clientDb);

                _clients.SaveChanges();
            }
        } 

        public void Delete(Guid id)
        {
            var client = _clients.Clients.FirstOrDefault(x => x.Id == id);

            IsClientInDatabase(client);

            _clients.Clients.Remove(client);

            _clients.SaveChanges();
        }

        public void AddAccount(Guid clientId, Account account)
        {
            var client = _clients.Clients.FirstOrDefault(x => x.Id == clientId);

            IsClientInDatabase(client);

            var accountConfig = new MapperConfiguration(cfg => cfg.CreateMap<AccountDb, Account>());

            var accountMapper = new Mapper(accountConfig);

            var accountDb = accountMapper.Map<AccountDb>(account);
            accountDb.Id = clientId;

            IsAccountInDatabase(accountDb);

            client.Accounts.Add(accountDb); 
            _clients.Accounts.Add(accountDb);

            _clients.SaveChanges();
        }

        public void UpdateAccount(Guid clientId, Account account) 
        {
            var accountConfig = new MapperConfiguration(cfg => cfg.CreateMap<Account, AccountDb>()
            .ForMember(x => x.Client, f => new Client() 
            { 
                Id = clientId
            }
            ));

            var accountMapper = new Mapper(accountConfig);

            var accountDb = accountMapper.Map<AccountDb>(account);

            IsAccountInDatabase(accountDb);

            var getAccount = _clients.Accounts.FirstOrDefault(x => x.ClientId == clientId);

            _clients.Entry(getAccount).State = EntityState.Detached;

            _clients.Accounts.Update(accountDb);

            _clients.SaveChanges();
        }

        public void DeleteAccount(Guid clientId, Account account)
        {
            var accountConfig = new MapperConfiguration(cfg => cfg.CreateMap<AccountDb, Account>());

            var accountMapper = new Mapper(accountConfig);

            var accountDb = accountMapper.Map<AccountDb>(account);
            accountDb.Id = clientId;

            IsAccountInDatabase(accountDb);

            var deleteAccount = _clients.Accounts.Where(x => x.ClientId == clientId);

            if (deleteAccount.Contains(accountDb))
            {
                _clients.Accounts.Remove(accountDb);
            }

            var client = _clients.Clients.FirstOrDefault(x => x.Id == clientId);

            client.Accounts.Remove(accountDb);

            _clients.SaveChanges();
        }

        private void IsClientInDatabase(ClientDb client)
        {
            var clientDb = _clients.Clients.FirstOrDefault(x => x.Id == client.Id);

            if (!_clients.Clients.Contains(clientDb))
            {
                throw new ArgumentException("Клиент не найден!");
            }
        }

        private void IsAccountInDatabase(AccountDb account)
        {
            if (!_clients.Accounts.Contains(_clients.Accounts.FirstOrDefault(x => x.Id == account.Id)))
            {
                throw new ArgumentException("Аккаунт не найден!");
            }
        }

        public void AddClient(Client client)
        {
            ClientDb clientDb = new ClientDb() {Id = Guid.NewGuid()};

            var defaultAccount = new AccountDb()
            {
                CurrencyName = "USD",
                Amount = 0,
                ClientId = clientDb.Id,
                Id = Guid.NewGuid()
            };

            var clientConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDb>()
                .ForMember(x => x.Accounts, f => new AccountDb()
                {
                    CurrencyName = "USD",
                    Amount = 0,
                    ClientId = clientDb.Id
                }));

            var clientMapper = new Mapper(clientConfig);

            clientDb = clientMapper.Map<ClientDb>(client);
            clientDb.Id = Guid.NewGuid();

            if (client.BirthDate > DateTime.Parse("31.12.2004"))
            {
                throw new AgeLimitException("Возраст клиента должен быть больше 18!");
            }

            if (client.Passport == 0)
            {
                throw new PassportNullException("Нельзя добавить клиента без паспортных данных!");
            }

            if (!_clients.Clients.Contains(clientDb))
            {
                defaultAccount.Client = clientDb;

                _clients.Clients.Add(clientDb);
                _clients.Accounts.Add(defaultAccount);

                _clients.SaveChanges();
            }
        }
    }
}
    
