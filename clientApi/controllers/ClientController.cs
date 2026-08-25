using ClientApi.Services.Interfaces;
using ClientApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientApi.Controllers;

[ApiController]

[Route("api/[controller]")]

public class ClientController : ControllerBase {


private readonly IClientService  _clientService;

public ClientController(IClientService ClientService){

    _clientService=ClientService;
}


[Authorize]
[HttpGet("{id}")]
public async Task<IActionResult> GetClientById(int id){

 var client = await _clientService.GetClientById(id);

 if(client==null) return NotFound();

 return Ok(client);

    }

   // [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllClients()
    {
        var clientList = await _clientService.GetAllClients();

        if(clientList==null) return NotFound();
        
        return Ok(clientList);

    }

[HttpPost]
public async Task<IActionResult> CreateClient([FromBody] Client client){

        var createdClient = await _clientService.CreateClient(client);

        return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);    
}






}
