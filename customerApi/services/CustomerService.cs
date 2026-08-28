using CustomerApi.Models;
using CustomerApi.Repositories.Interfaces;
using CustomerApi.Services.Interfaces;

namespace CustomerApi.Services;



public class CustomerService : ICustomerService
{


    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {

        _customerRepository = customerRepository;
    }

    public async Task<Customer?> GetClientById(int Id)
    {

        return await _customerRepository.GetClientById(Id);

    }

    public async Task<List<Customer>> GetAllClients() {

        return await _customerRepository.GetAllClients(); 
    }


    public async Task<Customer> CreateClient(Customer customer)
    {

        await CheckForDuplicateClient(customer);

        await _customerRepository.CreateClient(customer);
        await _customerRepository.SaveChangesAsync();
        return customer;

    }

    private async Task CheckForDuplicateClient(Customer customer)
    {

        if (await _customerRepository.EmailExistsAsync(customer.Mail))
        {
            throw new InvalidOperationException("A client with this email already exists");
        }


        if (await _customerRepository.PhoneExistsAsync(customer.Phone))
        {
            throw new InvalidOperationException("A client with this phone already exists");
        }


    }


}