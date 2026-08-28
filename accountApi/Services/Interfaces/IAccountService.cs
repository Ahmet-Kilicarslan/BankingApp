using AccountApi.Models;
using AccountApi.Models.DTOs;
namespace AccountApi.Services.Interfaces;


public interface IAccountService
{

    Task<Account?> GetAccountById(int Id);

    Task<Account> CreateAccount(Account account);

    Task UpdateBalance(BalanceUpdateDto balanceUpdateDto);

    Task<List<Account>> GetAllAccounts();
}