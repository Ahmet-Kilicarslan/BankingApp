using Microsoft.EntityFrameworkCore;
using TransactionApi.Models;
using TransactionApi.Repositories.Interfaces;
using TransactionApi.Data;

namespace TransactionApi.Repositories;

public class AccountRepository : IAccountRepository{


private readonly TransactionDbContext _context;

public AccountRepository( TransactionDbContext context){
    _context=context;
}

public async Task<Account?> GetById(int Id){

    return await _context.Accounts.FindAsync(Id);
}

public async Task AddAsync(Account account){

     await  _context.AddAsync(account);
}

public async Task SaveChangesAsync(){
     await  _context.SaveChangesAsync();  
}




}
