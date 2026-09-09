using AccountApi.Models;
using AccountApi.Models.DTOs;

namespace AccountApi.Repositories.Interfaces;

public interface IAccountRepository
{

    Task<Account?> GetAccountById(int Id);
    
    Task<AccountDetailsDto?> GetAccountDetailsByAccountNo(int AccountNo);
    Task<Account?> GetAccountByAccountNo(int accountNo);

    Task<List<AccountDetailsDto>> GetAccountsByCustomerId(int customerId);
     
    Task<List<AccountDetailsDto>> GetAllAccounts();
     
    Task<Account> CreateAccount(int customerId);

    Task SaveChangesAsync();

  

}