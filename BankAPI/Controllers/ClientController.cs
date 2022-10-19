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
        public async Task<Client> GetClientAsync(Guid clientId)
        {
            return await _clientService.GetClientByIdAsync(clientId);
        }

        [HttpPost]
        public void AddClientAsync(Client client)
        {
            _clientService.AddClientAsync(client);
        }

        [HttpPut]
        public async void UpdateClientAsync(Guid clientId, Client client)
        {
            await _clientService.UpdateAsync(clientId, client);
        }

        [HttpDelete]
        public async void DeleteClientAsync(Guid clientId)
        {
            await _clientService.DeleteAsync(clientId);
        }
    }
}