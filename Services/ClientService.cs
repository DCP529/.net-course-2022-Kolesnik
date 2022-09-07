using Models;
using Services.Exceptions;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ClientService 
    {
        private ClientStorage _clients = new ClientStorage();

        public ClientService(ClientStorage clientStorage)
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

            _clients.AddClient(client);
        }

        

        public Dictionary<Client, List<Account>> GetClients(ClientFilter clientFilters)
        {
            if (clientFilters.FirstName != null && clientFilters.LastName != null && clientFilters.Patronymic != null)
            {
                return _clients.clients.Where(x => x.Key.FirstName == clientFilters.FirstName)
                .Where(x => x.Key.LastName == clientFilters.LastName)
                .Where(x => x.Key.Patronymic == clientFilters.Patronymic).ToDictionary(t => t.Key, t => t.Value);
            }

            if (clientFilters.Passport != 0)
            {
                return _clients.clients.Where(x => x.Key.Passport == clientFilters.Passport).ToDictionary(t => t.Key, t => t.Value);
            }

            if (clientFilters.Phone != 0)
            {
                return _clients.clients.Where(x => x.Key.Phone == clientFilters.Phone).ToDictionary(t => t.Key, t => t.Value);
            }

            if (clientFilters.BirthDayRange != null)
            {
                return _clients.clients
                    .Where(x => x.Key.BirthDate >= clientFilters.BirthDayRange[0] && x.Key.BirthDate <= clientFilters.BirthDayRange[1])
                    .ToDictionary(t => t.Key, t => t.Value);
            }

            return _clients.clients.Where(x => x.Key.FirstName == clientFilters.FirstName)
                .Where(x => x.Key.LastName == clientFilters.LastName)
                .Where(x => x.Key.Patronymic == clientFilters.Patronymic)
                .Where(x => x.Key.Passport == clientFilters.Passport)
                .Where(x => x.Key.Phone == clientFilters.Phone)
                .Where(x => x.Key.BirthDate >= clientFilters.BirthDayRange[0] && x.Key.BirthDate <= clientFilters.BirthDayRange[1])
                .ToDictionary(t => t.Key, t => t.Value);
        }
    }
}
