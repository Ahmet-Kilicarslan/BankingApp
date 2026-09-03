
using CustomerApi.Models;
using CustomerApi.Repositories.Interfaces;
using System.Text.Json;

namespace CustomerApi.Services;




public class FileExporterService
{
    
    
    private readonly ICustomerRepository _customerRepository;
    
    
    public FileExporterService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }



    public async Task ExportCustomers()
    {
        var customers = await _customerRepository.GetAllClients();
        
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string filePath = Path.Combine(desktopPath, "ExistingCustomers.json");
        
        
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(customers, options);

        await File.WriteAllTextAsync(filePath, jsonString);



    }
    
    
    
}