using Microsoft.EntityFrameworkCore;
using TransactionApi.Models;
using TransactionApi.Data;
using TransactionApi.Repositories.Interfaces;

namespace TransactionApi.Repositories;

public class TransactionRepository : ITransactionRepository{

private readonly TransactionDbContext _context;

public TransactionRepository(TransactionDbContext context){
    _context=context;
}

public async Task<Transaction?> GetById(int Id){

    return await _context.Transactions.FindAsync(Id);
}


public async Task AddAsync(Transaction transaction){

     await _context.AddAsync(transaction);
}

/*public async Task SaveChangesAsync(){
     await  _context.SaveChangesAsync(); 
}*/




}