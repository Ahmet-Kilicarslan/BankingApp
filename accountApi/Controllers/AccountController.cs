using AccountApi.Models;
using AccountApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AccountApi.Models;
using AccountApi.Services.Interfaces;

namespace AccountApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{

    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetAccountById(int id)
    {

        var account = await _accountService.GetAccountById(id);

        if (account == null) return NotFound();

        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] Account account)
    {

        var createdAccount = await _accountService.CreateAccount(account);

        return CreatedAtAction(nameof(GetAccountById), new { id = createdAccount.Id }, createdAccount);
    }

}