using Microsoft.EntityFrameworkCore;
using AccountApi.Models;
namespace AccountApi.Data;

public class AccountDbContext : DbContext
{


    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
}