using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ClientService
    {
        private Dictionary<Client, List<Account>> _clients = new Dictionary<Client, List<Account>>();

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

            _clients.Add(client, accountList);
        }
    }
}
