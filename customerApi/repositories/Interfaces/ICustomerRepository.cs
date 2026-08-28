using CustomerApi.Models;

namespace CustomerApi.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetClientById(int Id);

    Task<List<Customer>> GetAllClients();
    

    Task CreateClient(Customer customer);
    
    Task SaveChangesAsync();

    Task<bool> EmailExistsAsync(string email);
    Task<bool> PhoneExistsAsync(string phone);

}