using AccountApi.Models;
using AccountApi.Repositories.Interfaces;
using AccountApi.Data;
using Microsoft.EntityFrameworkCore;


namespace TransactionApi.Repositories;

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

    public async Task CreateAccount(Account account)
    {

        await _context.AddAsync(account);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }




}
