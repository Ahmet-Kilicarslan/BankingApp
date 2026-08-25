



using AccountApi.Models.DTOs;
using System.Net.Http.Json;


namespace AccountApi.Services;

public class TokenService
{

    private readonly IHttpClientFactory _httpClientFactory;

    public TokenService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
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
            new { CallerId = "account-api" }
        );


        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TokenResponseDTO>();

        return result.Token;

    }


}