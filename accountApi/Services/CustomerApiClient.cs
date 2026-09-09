using AccountApi.Models.DTOs;

namespace AccountApi.Services;


public class CustomerApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TokenService _tokenService;


    public CustomerApiClient(TokenService tokenService, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }
    
    public async Task<CustomerDto> GetCustomerDetailsByCustomerId(int customerId)
    {
        var httpClient = _httpClientFactory.CreateClient("CustomerApi");
        var token = await _tokenService.GetTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"/api/customer/{customerId}");

        return await response.Content.ReadFromJsonAsync<CustomerDto>()
               ?? throw new InvalidOperationException($"Account {customerId} returned an empty response.");
    }
    
    public async Task<bool> CustomerExists(int customerId)
    {
        var httpClient = _httpClientFactory.CreateClient("CustomerApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"/api/customer/{customerId}");

        return response.IsSuccessStatusCode;
    }
    public async Task<string> GetCustomerName(int customerId)
    {
        var httpClient = _httpClientFactory.CreateClient("CustomerApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"/api/customer/{customerId}");
        var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();

        return customer.Name;
    }
}