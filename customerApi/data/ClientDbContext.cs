using Microsoft.EntityFrameworkCore;
using CustomerApi.Models;

namespace CustomerApi.Data;

public class ClientDbContext: DbContext
{

public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options){

}

public DbSet<Customer> Clients{get;set;}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Customer>()
        .HasIndex(c => c.Mail)
        .IsUnique();
}

}