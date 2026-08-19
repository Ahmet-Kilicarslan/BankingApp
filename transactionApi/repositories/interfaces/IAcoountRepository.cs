using TransactionApi.Models;
namespace TransactionApi.Repositories.Interfaces;

public interface IAccountRepository{

Task<Account?> GetById(int Id);

Task AddAsync(Account account);

Task SaveChangesAsync();

}