using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;
using CustomerApi.Models;
using CustomerApi.Repositories.Interfaces;

namespace CustomerApi.Repositories;

public class CustomerRepository : ICustomerRepository
{

    private readonly ClientDbContext _context;


    public CustomerRepository(ClientDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetClientById(int id)
    {

        return await _context.Clients.FindAsync(id);
    }

    public async Task<List<Customer>> GetAllClients()
    {

        List<Customer> clientList = await _context.Clients.ToListAsync();

        return clientList;

    }

    public async Task CreateClient(Customer customer)
    {


        await _context.AddAsync(customer);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {

        return await _context.Clients.AnyAsync(c => c.Mail == email);

    }

    public async Task<bool> PhoneExistsAsync(string phone)
    {

        return await _context.Clients.AnyAsync(c => c.Phone == phone);

    }





}