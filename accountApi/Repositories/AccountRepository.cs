using AccountApi.Models;
using AccountApi.Repositories.Interfaces;
using AccountApi.Data;
using Microsoft.EntityFrameworkCore;
using AccountApi.Models.DTOs;

namespace AccountApi.Repositories;

public class AccountRepository : IAccountRepository
{


    private readonly AccountDbContext _context;

    public AccountRepository(AccountDbContext context)
    {
        _context = context;
    }
    
    

    public async Task<Account?> GetAccountById(int Id)
    {

        return await _context.Accounts.FindAsync(Id);
    }

    public async Task<List<Account>> GetAccountsByCustomerId(int customerId)
    {

        List<Account> accountList = await _context.Accounts.Where(account => account.CustomerId == customerId).ToListAsync();
        
        return accountList;

    }

    public async Task<List<Account>> GetAllAccounts()
    {
        List<Account> accountList = await _context.Accounts.ToListAsync();

        return accountList;
    }

    public async Task CreateAccount(Account account)
    {

        await _context.AddAsync(account);
    }



    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

  
   



}
