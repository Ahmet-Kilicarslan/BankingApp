using ClientApi.Models;

namespace ClientApi.Repositories;

public interface IClientRepository
{
    Task<Client?> getClientById(int Id);
    

    Task CreateClient(Client Client);
    
    Task SaveChangesAsync();

    Task<bool> EmailExistsAsync(string email);
    Task<bool> PhoneExistsAsync(string phone);

}