using Microsoft.EntityFrameworkCore;
using TransactionApi.Models;

namespace TransactionApi.Data;

public class TransactionDbContext : DbContext
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes{get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<TransactionType>().HasData(
        new TransactionType { Id = 1, Name = "Deposit" },
        new TransactionType { Id = 2, Name = "Withdraw" }
    );
}
}