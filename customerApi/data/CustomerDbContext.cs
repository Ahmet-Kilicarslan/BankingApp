using Microsoft.EntityFrameworkCore;
using CustomerApi.Models;

namespace CustomerApi.Data;

public class CustomerDbContext: DbContext
{

public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options){

}

public DbSet<Customer> Customers{get;set;}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Customer>()
        .HasIndex(c => c.Mail)
        .IsUnique();
}

}