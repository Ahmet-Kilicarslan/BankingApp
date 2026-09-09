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
    private readonly CustomerApiClient _customerApiClient;


    public AccountService(
        IAccountRepository accountRepository
        , CustomerApiClient customerApiClient
    )
    {
        _accountRepository = accountRepository;
        _customerApiClient = customerApiClient;
    }

    public async Task<Account?> GetAccountById(int id)
    {
        return await _accountRepository.GetAccountById(id);
    }


    public async Task<List<AccountDetailsDto>> GetAccountsByCustomerId(int customerId)
    {
        return await _accountRepository.GetAccountsByCustomerId(customerId);
    }

    public async Task<AccountDetailsDto?> GetAccountDetailsByAccountNo(int accountNo)
    {
        var accountDetails = await _accountRepository.GetAccountDetailsByAccountNo(accountNo);

        var customerName = await _customerApiClient.GetCustomerName(accountDetails.CustomerId);

        accountDetails.CustomerName = customerName;
        return accountDetails;
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
            var customerName = await _customerApiClient.GetCustomerName(account.CustomerId);

            account.CustomerName = customerName;

            return account;
        });

        var accountDetailsArray = await Task.WhenAll(tasks);

        return accountDetailsArray.ToList();
    }


    public async Task<Account> CreateAccount(int customerId)
    {
        if (!await _customerApiClient.CustomerExists(customerId))
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
}