using TransactionApi.Models;
using TransactionApi.Data;
using TransactionApi.Repositories.Interfaces;

namespace TransactionApi.Repositories;

public class TransactionRepository : ITransactionRepository{

private readonly TransactionDbContext _context;

public TransactionRepository(TransactionDbContext context){
    _context=context;
}

public async Task<Transaction?> GetTransactionById(int Id){

    return await _context.Transactions.FindAsync(Id);
}


public async Task CreateTransaction(Transaction transaction){

     await _context.AddAsync(transaction);
}

public async Task SaveChangesAsync(){
     await  _context.SaveChangesAsync(); 
}




}