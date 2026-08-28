using CustomerApi.Models;
using CustomerApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers;

[ApiController]

[Route("api/[controller]")]

public class CustomerController : ControllerBase {


private readonly ICustomerService  _customerService;

public CustomerController(ICustomerService customerService){

    _customerService=customerService;
}


[Authorize]
[HttpGet("{id}")]
public async Task<IActionResult> GetClientById(int id){

 var client = await _customerService.GetClientById(id);

 if(client==null) return NotFound();

 return Ok(client);

    }

   // [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllClients()
    {
        var clientList = await _customerService.GetAllClients();

        if(clientList==null) return NotFound();
        
        return Ok(clientList);

    }

[HttpPost]
public async Task<IActionResult> CreateClient([FromBody] Customer customer){

        var createdClient = await _customerService.CreateClient(customer);

        return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);    
}






}
