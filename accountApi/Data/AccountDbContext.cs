using Microsoft.EntityFrameworkCore;
using AccountApi.Models;
namespace AccountApi.Data;

public class AccountDbContext : DbContext
{


    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNo)
            .IsUnique();
    }
}