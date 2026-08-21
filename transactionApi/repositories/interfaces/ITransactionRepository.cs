using TransactionApi.Models;

namespace TransactionApi.Repositories.Interfaces;


public interface ITransactionRepository{

public Task<Transaction?> GetTransactionById(int Id);

public Task CreateTransaction(Transaction transaction);

 public Task SaveChangesAsync();

}