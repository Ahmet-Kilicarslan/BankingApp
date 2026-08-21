using TransactionApi.Models;
namespace TransactionApi.Repositories.Interfaces;

public interface IAccountRepository{

Task<Account?> GetAccountById(int Id);

Task CreateAccount(Account account);

Task SaveChangesAsync();

}