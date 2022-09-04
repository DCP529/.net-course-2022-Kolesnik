using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ClientService
    {
        private Dictionary<Client, List<Account>> _clients;

        public void AddClient(Client client, params Account[] accounts)
        {
            try
            {
                if (client.BirthDate > DateTime.Parse("31.12.2004"))
                {
                    throw new AgeLimitException("Возраст клиента должен быть больше 18!");
                }

                if (client.Passport == 0)
                {
                    throw new PassportNullException("Нельзя добавить клиента без паспортных данных!");
                }

                var accountsList = new List<Account>();

                for (int i = 0; i < accounts.Length; i++)
                {
                    accountsList.Add(accounts[i]);
                }

                _clients.Add(client, accountsList);
            }
            catch (AgeLimitException ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
            catch (PassportNullException ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

        }
    }
}
