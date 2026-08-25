using ClientApi.Models;

namespace ClientApi.Services.Interfaces;


public interface IClientService
{
    Task<Client?> GetClientById(int Id);

    Task<Client> CreateClient(Client client);

    Task<List<Client>> GetAllClients();


}
    
