using AccountApi.Models;
using AccountApi.Models.DTOs;
namespace AccountApi.Services.Interfaces;


public interface IAccountService
{

    Task<Account?> GetAccountById(int id);
    
    Task<Account?> GetAccountByAccountNo(int accountNo);

    Task<List<AccountDetailsDto>> GetAccountsByCustomerId(int customerId);
    
    Task<Account> CreateAccount(int customerId);

    Task UpdateBalance(AccountBalanceOperationDto dto);

    Task<List<AccountDetailsDto>> GetAllAccounts();
}