using Microsoft.AspNetCore.Mvc;
using TransactionApi.Services.Interfaces;
using TransactionApi.Models;
using TransactionApi.Models.DTOs;

namespace TransactionApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TransactionController : ControllerBase
{

    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        var transaction = await _transactionService.GetTransactionbyId(id);

        if (transaction == null) return NotFound();

        return Ok(transaction);

    }


    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] AccountBalanceOperationDto dto)
    {

        var createdTransaction = await _transactionService.CreateTransaction(dto);

        return CreatedAtAction(nameof(GetTransactionById), new { id = createdTransaction.Id }, createdTransaction);

    }



}