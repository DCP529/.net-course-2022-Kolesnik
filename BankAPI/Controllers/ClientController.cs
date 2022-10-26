using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Models;
using Models.ModelsDb;
using Services;
using Services.Filters;
using System.ComponentModel;

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
        public async Task<ActionResult<Client>> GetClientAsync(Guid clientId) 
        {
            if (await _clientService.GetClientByIdAsync(clientId) != null)
            {
                return await _clientService.GetClientByIdAsync(clientId);
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> AddClientAsync(Client client) 
        {
            _clientService.AddClientAsync(client);

            if (_clientService.GetClientByIdAsync(client.Id) != null)
            {
                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientAsync(Guid clientId, Client client)
        {
            await _clientService.UpdateAsync(clientId, client);

            var getClient = await _clientService.GetClientByIdAsync(clientId);

            if (getClient.Id != client.Id)
            {
                return new BadRequestObjectResult(ModelState);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClientAsync(Guid clientId)
        {
            await _clientService.DeleteAsync(clientId);

            var getClient = await _clientService.GetClientsAsync(new ClientFilter() { Id = clientId });

            if (getClient.Count != 1)
            {
                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }
    }
}