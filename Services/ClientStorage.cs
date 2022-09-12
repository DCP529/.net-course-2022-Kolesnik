using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ClientStorage
    {
        public readonly Dictionary<Client, List<Account>> clients = new Dictionary<Client, List<Account>>();

        public void AddClient(Client client)
        {            
            var accountList = new List<Account> { new Account()
            {
                Currency = new Currency()
                {
                    Code = 1,
                    Name = "USD"
                }
            } };

            clients.Add(client, accountList);
        }
    }
}
