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
            IEnumerable<KeyValuePair<Client, List<Account>>> query = _clients.clients.Select(t => t);

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
    }
}
