using System.Security.Principal;
using AccountApi.Models;
using AccountApi

namespace TransactionApi.Services;

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

    public async Task<Account> CreateAccount(Account account)
    {
        if (!await ClientExists(account.ClientId))
        {

            throw new InvalidOperationException("Client does not exist");

        }



        await _accountRepository.CreateAccount(account);
        await _accountRepository.SaveChangesAsync();
        return account;

    }


    private async Task<bool> ClientExists(int clientId)
    {

        var httpCLient = _httpClientFactory.CreateClient("ClientApi");

        var token = await _tokenService.GetTokenAsync();

        httpCLient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpCLient.GetAsync($"api/client/{clientId}");

        return response.IsSuccessStatusCode;


    }



}








