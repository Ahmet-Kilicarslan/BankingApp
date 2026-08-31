using System.Security.Principal;
using AccountApi.Models;
using AccountApi.Repositories.Interfaces;
using AccountApi.Services.Interfaces;
using AccountApi.Services;
using AccountApi.Models.DTOs;
using AccountApi.Middleware;
namespace AccountApi.Services;

public class AccountService : IAccountService
{

    private readonly IAccountRepository _accountRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly TokenService _tokenService;


    public AccountService(IAccountRepository accountRepository, IHttpClientFactory httpClientFactory, TokenService tokenService)
    {
        _accountRepository = accountRepository;
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }

    public async Task<Account?> GetAccountById(int Id)
    {

        return await _accountRepository.GetAccountById(Id);

    }

    public async Task<List<Account>> GetAccountsByCustomerId(int customerId)
    {
        return await _accountRepository.GetAccountsByCustomerId(customerId);
        
    }
    public async Task<List<Account>> GetAllAccounts()
    {

        return await _accountRepository.GetAllAccounts();

    }
    

    public async Task<Account> CreateAccount(Account account)
    {
        if (!await CustomerExists(account.CustomerId))
        {

            throw new InvalidOperationException("Client does not exist");

        }



        await _accountRepository.CreateAccount(account);
        await _accountRepository.SaveChangesAsync();
        return account;

    }


    public async Task UpdateBalance(BalanceUpdateDto balanceUpdateDto)
    {
        var account = await _accountRepository.GetAccountById(balanceUpdateDto.AccountId);

        if (account == null) {
            throw new InvalidOperationException("Account doesn't exist!");
        
        }

        if (balanceUpdateDto.TransactionTypeId == 1) //Deposit
        {
            account.Balance += balanceUpdateDto.Amount;


        }else if(balanceUpdateDto.TransactionTypeId == 2 ) //Withdraw
        {

            if (account.Balance < balanceUpdateDto.Amount) {
                throw new InvalidOperationException("Unsufficient balance!");
            }
            account.Balance -= balanceUpdateDto.Amount;

        }
        else throw new ArgumentException("Invalid transaction type.");


        await _accountRepository.SaveChangesAsync();

    }


  
    private async Task<bool> CustomerExists(int customerId)
    {

        var httpClient = _httpClientFactory.CreateClient("CustomerApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"api/customer/{customerId}");

        return response.IsSuccessStatusCode;


    }



}








