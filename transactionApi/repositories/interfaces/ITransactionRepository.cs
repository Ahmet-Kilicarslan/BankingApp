using TransactionApi.Models;
namespace TransactionApi.Repositories.Interfaces;


public interface ITransactionRepository{

public Task<Transaction?> GetById(int Id);
public Task AddAsync(Transaction transaction);
 //public Task SaveChangesAsync();

}