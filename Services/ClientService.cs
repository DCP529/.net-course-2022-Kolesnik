using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ClientService
    {
        private List<Client> _clients = new List<Client>();

        public void AddClient(Client client)
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

                _clients.Add(client);

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
