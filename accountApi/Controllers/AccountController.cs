using AccountApi.Models;
using AccountApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AccountApi.Models.DTOs;

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

    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        var accountList = await _accountService.GetAllAccounts();

        if(accountList == null) return NotFound();

        return Ok(accountList);

    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] Account account)
    {

        var createdAccount = await _accountService.CreateAccount(account);

        return CreatedAtAction(nameof(GetAccountById), new { id = createdAccount.Id }, createdAccount);
    }


    [Authorize]
    [HttpPost("update-balance")]

    public async Task<IActionResult> UpdateBalance([FromBody] BalanceUpdateDto balanceUpdateDto)
    {
        await _accountService.UpdateBalance(balanceUpdateDto);


        return Ok(balanceUpdateDto);
    }


}