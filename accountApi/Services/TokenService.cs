


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AccountApi.Models.DTOs;

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
        var httpClient = _httpClientFactory.CreateClient("AuthApi");

        var response = await httpClient.PostAsJsonAsync("/api/Token", new { clientId = "transaction-api" });
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TokenResponseDTO>();
        return result.Token;
    }


}