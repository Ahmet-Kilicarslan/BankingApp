using AccountApi.Models;
using AccountApi.Models.DTOs;

namespace AccountApi.Repositories.Interfaces;

public interface IAccountRepository
{

    Task<Account?> GetAccountById(int Id);

    Task<List<Account>> GetAllAccounts();
     
    Task CreateAccount(Account account);

    Task SaveChangesAsync();

  

}