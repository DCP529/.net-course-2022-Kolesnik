using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ModelsDb;
using Services;

namespace Controllers.Controllers
{
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        ClientService _clientService;

        public ClientController()
        {
            _clientService = new ClientService(new BankDbContext());
        }

        [HttpGet]
        public Client GetClient(Guid clientId)
        {
            return _clientService.GetClientById(clientId).Result;
        }

        [HttpPost]
        public void AddClient(Client client)
        {
            _clientService.AddClient(client);
        }

        [HttpPut]
        public void UpdateClient(Guid clientId, string firstName, string lastName)
        {
            _clientService.Update(clientId, new Client()
            {
                Id = clientId,
                FirstName = firstName,
                LastName = lastName
            });
        }

        [HttpDelete]
        public void DeleteClient(Guid clientId)
        {
            _clientService.Delete(clientId);
        }
    }
}