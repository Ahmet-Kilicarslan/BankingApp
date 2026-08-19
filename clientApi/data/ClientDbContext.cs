using Microsoft.EntityFrameworkCore;
using ClientApi.Models;

namespace ClientApi.Data;

public class ClientDbContext: DbContext
{

public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options){

}

public DbSet<Client> Clients{get;set;}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Client>()
        .HasIndex(c => c.Mail)
        .IsUnique();
}

}