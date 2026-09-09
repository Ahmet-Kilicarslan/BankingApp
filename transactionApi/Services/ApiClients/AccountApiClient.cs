using TransactionApi.Models;
using TransactionApi.Models.DTOs;


namespace TransactionApi.Services.ApiClients;


public class AccountApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TokenService _tokenService;


    public AccountApiClient(TokenService tokenService, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
        
        
    }


    public async Task<AccountDetailsDto> GetAccountDetailsByAccountNo(int accountNo)
    {
        
        var httpClient = _httpClientFactory.CreateClient("AccountApi");
        var token = await _tokenService.GetTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"/api/account/by-account-no/{accountNo}");

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Failed to fetch account {accountNo} from AccountApi. Status: {response.StatusCode}");
        }

        return await response.Content.ReadFromJsonAsync<AccountDetailsDto>()
               ?? throw new InvalidOperationException($"Account {accountNo} returned an empty response.");    
        
    }
    
    public async Task<bool> UpdateAccountBalance(AccountBalanceOperationDto accountBalanceOperationDto)
    {
        var httpClient = _httpClientFactory.CreateClient("AccountApi");

        var token = await _tokenService.GetTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


        var response = await httpClient.PostAsJsonAsync("/api/Account/Update-Balance", accountBalanceOperationDto);

        return response.IsSuccessStatusCode;
    }
}
        
