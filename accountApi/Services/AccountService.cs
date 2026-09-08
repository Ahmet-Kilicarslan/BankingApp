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


    public AccountService(IAccountRepository accountRepository, IHttpClientFactory httpClientFactory,
        TokenService tokenService)
    {
        _accountRepository = accountRepository;
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }

    public async Task<Account?> GetAccountById(int id)
    {
        return await _accountRepository.GetAccountById(id);
    }


    public async Task<List<AccountDetailsDto>> GetAccountsByCustomerId(int customerId)
    {
        return await _accountRepository.GetAccountsByCustomerId(customerId);
    }

    public async Task<Account?> GetAccountByAccountNo(int accountNo)
    {
        return await _accountRepository.GetAccountByAccountNo(accountNo);
    }

    public async Task<List<AccountDetailsDto>> GetAllAccounts()
    {
        var accounts = await _accountRepository.GetAllAccounts();

        var tasks = accounts.Select(async account =>
        {
            var customerName = await GetCustomerName(account.CustomerId);

            account.CustomerName = customerName;

            return account;
        });

        var accountDetailsArray = await Task.WhenAll(tasks);

        return accountDetailsArray.ToList();
    }

    private async Task<string> GetCustomerName(int customerId)
    {
        var httpClient = _httpClientFactory.CreateClient("CustomerApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"/api/customer/{customerId}");
        var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();

        return customer.Name;
    }

    public async Task<Account> CreateAccount(int customerId)
    {
        if (!await CustomerExists(customerId))
        {
            throw new InvalidOperationException("Client does not exist");
        }


        var account = await _accountRepository.CreateAccount(customerId);
        await _accountRepository.SaveChangesAsync();
        return account;
    }


    public async Task UpdateBalance(AccountBalanceOperationDto dto)
    {
        var account = await _accountRepository.GetAccountByAccountNo(dto.AccountNo);

        if (account == null)
        {
            throw new InvalidOperationException("Account doesn't exist!");
        }

        if (dto.Role == BalanceOperationRole.Receiver)
        {
            account.Balance += dto.Amount;
        }
        else if (dto.Role == BalanceOperationRole.Sender)
        {
            if (account.Balance < dto.Amount)
            {
                throw new InvalidOperationException($"Balance {account.Balance} is less than  {dto.Amount}");
            }

            account.Balance -= dto.Amount;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(dto.Role), dto.Role, "Unhandled balance operation role.");
        }


        await _accountRepository.SaveChangesAsync();
    }


    private async Task<bool> CustomerExists(int customerId)
    {
        var httpClient = _httpClientFactory.CreateClient("CustomerApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"/api/customer/{customerId}");

        return response.IsSuccessStatusCode;
    }
}