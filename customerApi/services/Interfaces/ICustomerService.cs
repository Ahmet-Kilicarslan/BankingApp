using CustomerApi.Models;

namespace CustomerApi.Services.Interfaces;


public interface ICustomerService
{
    Task<Customer?> GetClientById(int Id);

    Task<Customer> CreateClient(Customer customer);

    Task<List<Customer>> GetAllClients();


}
    
