using ClientApi.Models;

namespace ClientApi.Services;


public interface IClientService
{
    Task<Client?> GetClientById(int Id);

    Task<Client> CreateClient(Client client);
    


}
    
