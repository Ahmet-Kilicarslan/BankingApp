using Microsoft.EntityFrameworkCore;
using ClientApi.Data;
using ClientApi.Models;
using ClientApi.Repositories;

namespace ClientApi.Repositories;

public class ClientRepository : IClientRepository  {

private readonly ClientDbContext _context;


public ClientRepository(ClientDbContext context){
    _context=context;
      }

public async Task<Client?> getClientById(int id){

    return await _context.Clients.FindAsync(id);
}

public async Task CreateClient(Client client){


     await _context.AddAsync(client);
}

   public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

public async Task<bool> EmailExistsAsync(string email){
    
        return await _context.Clients.AnyAsync(c => c.Mail == email);

}

public async Task<bool> PhoneExistsAsync(string phone){

return await _context.Clients.AnyAsync(c=>c.Phone == phoen);    

}





}