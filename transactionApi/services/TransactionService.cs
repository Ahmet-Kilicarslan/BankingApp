using TransactionApi.Models;
using TransactionApi.Repositories.Interfaces;
using TransactionApi.Services.Interfaces;
using TransactionApi.Models.DTOs;

namespace TransactionApi.Services;

public class TransactionService : ITransactionService
{

    private readonly ITransactionRepository _transactionRepository;

    private readonly IHttpClientFactory _httpClientFactory;

    private readonly TokenService _tokenService;


    public TransactionService(ITransactionRepository transactionRepository, IHttpClientFactory httpClientFactory, TokenService tokenService)
    {
        _transactionRepository = transactionRepository;
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;


    }



    public async Task<Transaction?> GetTransactionbyId(int Id)
    {

        return await _transactionRepository.GetTransactionById(Id);

    }


    public async Task<List<Transaction>> GetAllTransactions()
    {
        return await _transactionRepository.GetAllTransactions();
    }

    public async Task<Transaction> CreateTransaction(AccountBalanceOperationDto dto)
    {




     

        Boolean BalanceChanged = await UpdateAccountBalance(dto);


        if (!BalanceChanged)
        {

            throw new InvalidOperationException("Failed to update account balance");

        }

        var transaction = new Transaction
        {
            AccountId = dto.AccountId,
            Amount = dto.Amount,
            TransactionTypeId = dto.TransactionTypeId
        };



        await _transactionRepository.CreateTransaction(transaction);
        await _transactionRepository.SaveChangesAsync();

        return transaction;
    }


  
    private async Task<bool> UpdateAccountBalance(AccountBalanceOperationDto accountBalanceOperationDto)
    {

        var httpClient = _httpClientFactory.CreateClient("AccountApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



        var response = await httpClient.PostAsJsonAsync("/api/Account/Update-Balance", accountBalanceOperationDto);

        return response.IsSuccessStatusCode;



    }



}