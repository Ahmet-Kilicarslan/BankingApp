using AccountApi.Models;
using AccountApi.Models.DTOs;
namespace AccountApi.Services.Interfaces;


public interface IAccountService
{

    Task<Account?> GetAccountById(int Id);

    Task<List<Account>> GetAccountsByCustomerId(int customerId);
    
    Task<Account> CreateAccount(int customerId);

    Task UpdateBalance(BalanceUpdateDto balanceUpdateDto);

    Task<List<Account>> GetAllAccounts();
}