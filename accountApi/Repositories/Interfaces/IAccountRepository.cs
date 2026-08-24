using AccountApi.Models;


namespace AccountApi.Repositories.Interfaces;

public interface IAccountRepository
{

    Task<Account?> GetAccountById(int Id);

    Task CreateAccount(Account account);

    Task SaveChangesAsync();

}