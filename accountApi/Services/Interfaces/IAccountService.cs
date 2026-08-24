using AccountApi.Models;

namespace AccountApi.Services.Interfaces;


public interface IAccountService
{

    Task<Account?> GetAccountById(int Id);

    Task<Account> CreateAccount(Account account);

}