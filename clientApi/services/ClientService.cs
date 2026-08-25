using ClientApi.Repositories.Interfaces;
using ClientApi.Services.Interfaces;
using ClientApi.Models;

namespace ClientApi.Services;



public class ClientService : IClientService
{


    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {

        _clientRepository = clientRepository;
    }

    public async Task<Client?> GetClientById(int Id)
    {

        return await _clientRepository.GetClientById(Id);

    }

    public async Task<List<Client>> GetAllClients() {

        return await _clientRepository.GetAllClients(); 
    }


    public async Task<Client> CreateClient(Client client)
    {

        await CheckForDuplicateClient(client);

        await _clientRepository.CreateClient(client);
        await _clientRepository.SaveChangesAsync();
        return client;

    }

    private async Task CheckForDuplicateClient(Client client)
    {

        if (await _clientRepository.EmailExistsAsync(client.Mail))
        {
            throw new InvalidOperationException("A client with this email already exists");
        }


        if (await _clientRepository.PhoneExistsAsync(client.Phone))
        {
            throw new InvalidOperationException("A client with this phone already exists");
        }


    }


}