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

    public async Task<List<AccountDetailsDto>> GetAccountsByCustomerId(int customerId)
    {
        List<Account> accounts =
            await _context.Accounts.Where(account => account.CustomerId == customerId)
                .Include(a => a.Currency)
                .Include(a => a.Bank)
                .ToListAsync();

        List<AccountDetailsDto> accountList = new List<AccountDetailsDto>();

        foreach (Account account in accounts)
        {
            var accountDetails = new AccountDetailsDto(
                account.Id,
                account.AccountNo,
                account.CustomerId,
                null,
                account.Balance,
                account.Currency.Name,
                account.Bank.Name,
                account.OpenedAt
            );
            accountList.Add(accountDetails);
        }

        return accountList;
    }


    public async Task<Account?> GetAccountByAccountNo(int accountNo)
    {
        return await _context.Accounts
            .SingleOrDefaultAsync(a => a.AccountNo == accountNo);
    }

    public async Task<List<AccountDetailsDto>> GetAllAccounts()
    {
        List<Account> accounts = await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.Bank)
            .ToListAsync();

        List<AccountDetailsDto> accountList = new List<AccountDetailsDto>();

        foreach (Account account in accounts)
        {
            var accountDetails = new AccountDetailsDto(
                account.Id,
                account.AccountNo,
                account.CustomerId,
                null,
                account.Balance,
                account.Currency.Name,
                account.Bank.Name,
                account.OpenedAt
            );
            accountList.Add(accountDetails);
        }

        return accountList;
    }

    public async Task<Account> CreateAccount(int customerId)
    {
        var account = new Account
        {
            AccountNo = GenerateAccountNo(),
            CustomerId = customerId,
            Balance = 0
        };

        await _context.AddAsync(account);

        return account;
    }


    private int GenerateAccountNo()
    {
        return Random.Shared.Next(100000, 1000000);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}