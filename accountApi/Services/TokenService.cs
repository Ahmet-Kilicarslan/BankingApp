using AccountApi.Models.DTOs;
using System.Net.Http.Json;


namespace AccountApi.Services;

public class TokenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<string> GetTokenAsync()
    {
        /* var httpClient = _httpClientFactory.CreateClient("AuthApi");

         var response = await httpClient.PostAsJsonAsync("/api/Token", new { CallerId = "account-api" });

         response.EnsureSuccessStatusCode();

         var result = await response.Content.ReadFromJsonAsync<TokenResponseDTO>();
         return result.Token;*/

        var httpClient = _httpClientFactory.CreateClient("AuthApi");

        var response = await httpClient.PostAsJsonAsync(
            "/api/Token",
            new
            {
                ClientId = _configuration["OAuthClient:ClientId"],
                clientSecret = _configuration["OAuthClient:ClientSecret"]
            }
        );


        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TokenResponseDTO>();

        return result.Token;
    }
}