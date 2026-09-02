using AccountApi.Models;
using AccountApi.Models.DTOs;

namespace AccountApi.Repositories.Interfaces;

public interface IAccountRepository
{

    Task<Account?> GetAccountById(int Id);

    Task<List<Account>> GetAccountsByCustomerId(int customerId);
     
    Task<List<Account>> GetAllAccounts();
     
    Task<Account> CreateAccount(int customerId);

    Task SaveChangesAsync();

  

}