using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TransactionApi.Models.DTOs;

namespace TransactionApi.Services;

public class TokenService
{

    private readonly IHttpClientFactory _httpClientFactory;

    private readonly IConfiguration _configuration;

    public TokenService(IHttpClientFactory httpClientFactory,IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
          _configuration = configuration;
    
    }

    public async Task<string> GetTokenAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("AuthApi");




        var response = await httpClient.PostAsJsonAsync(
          "/api/Token",
          new
          {
              ClientId = _configuration["OAuthClient:ClientId"],
              clientSecret = _configuration["OAuthClient:ClientSecret"]
          });


        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException($"AuthApi token request failed: {errorBody}");
        }

        var result = await response.Content.ReadFromJsonAsync<TokenResponseDTO>();
        return result.Token;
    }


}