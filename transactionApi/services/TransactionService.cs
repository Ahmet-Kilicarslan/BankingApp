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

    public async Task<List<TransactionDetailsDto>> GetAllTransactionDetails()
    {
        var transList = await _transactionRepository.GetAllTransactions();

        List<TransactionDetailsDto> transDetailsList = new List<TransactionDetailsDto>();

        foreach (var item in transList)
        {
            var accountDetails = await GetAccountDetailsByAccountId(item.AccountId);
            var customerDetails = await GetCustomerDetailsByCustomerId(accountDetails.CustomerId);
            string transTypeName = await GetTransactionTypeName(item.TransactionTypeId);

            var transDetail = new TransactionDetailsDto
            {
                CustomerName = customerDetails.Name,
                AccountNo = accountDetails.AccountNo,
                TransactionType = transTypeName,
                Amount = item.Amount,
                TransactionDate = item.TransactionDate
            };
            
            transDetailsList.Add(transDetail);
            

        }
        
        
        return transDetailsList;
    }


    private async Task<string> GetTransactionTypeName(int transactionTypeId)
    {
       return await _transactionRepository.GetTransactionTypeName(transactionTypeId);
        
    }
  
    private async Task<bool> UpdateAccountBalance(AccountBalanceOperationDto accountBalanceOperationDto)
    {

        var httpClient = _httpClientFactory.CreateClient("AccountApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



        var response = await httpClient.PostAsJsonAsync("/api/Account/Update-Balance", accountBalanceOperationDto);

        return response.IsSuccessStatusCode;



    }
    
    
    private async Task<AccountDetailsDto> GetAccountDetailsByAccountId(int accountId)
    {
        var httpClient = _httpClientFactory.CreateClient("AccountApi");
        var token = await _tokenService.GetTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.GetAsync($"/api/account/{accountId}" );
        
        return await response.Content.ReadFromJsonAsync<AccountDetailsDto>()
               ?? throw new InvalidOperationException($"Account {accountId} returned an empty response.");
            
    }


    private async Task<CustomerDetailsDto> GetCustomerDetailsByCustomerId(int customerId)
    {
        var httpClient = _httpClientFactory.CreateClient("CustomerApi");
        var token = await _tokenService.GetTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.GetAsync($"/api/customer/{customerId}" );
        
        return await response.Content.ReadFromJsonAsync<CustomerDetailsDto>()
               ?? throw new InvalidOperationException($"Account {customerId} returned an empty response.");

        
    }
    
    



}