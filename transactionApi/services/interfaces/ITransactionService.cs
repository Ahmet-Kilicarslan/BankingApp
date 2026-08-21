using TransactionApi.Models;

namespace TransactionApi.Services.Interfaces;



public interface ITransactionService{

Task<Transaction?> GetTransactionbyId(int Id);

Task<Transaction> CreateTransaction(Transaction Transaction);


}