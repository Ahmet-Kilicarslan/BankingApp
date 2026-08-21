using TransactionApi.Models;
using TransactionApi.Repositories.Interfaces;
using TransactionApi.Services.Interfaces;

namespace TransactionApi.Services;

public class TransactionService : ITransactionService{

private readonly ITransactionRepository _transactionRepository;

public TransactionService(ITransactionRepository transactionRepository){

    _transactionRepository= transactionRepository;
}

public async Task<Transaction?> GetTransactionbyId(int Id){
 
 return await _transactionRepository.GetTransactionById( Id);

}

public async Task<Transaction> CreateTransaction(Transaction transaction){
    
    await _transactionRepository.CreateTransaction(transaction);
    await _transactionRepository.SaveChangesAsync();
    return transaction;
}



}