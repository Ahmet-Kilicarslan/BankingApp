using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Services;

public class TokenService
{

    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration) { 
    
        _configuration = configuration; 
    }

    public string GenerateToken(string CallerId)
    {
        var privateKey = _configuration["Jwt:PrivateKey"] 
            ?? throw new InvalidOperationException(
            "JWT private key is not configured.");

        byte[] privateKeyBytes = Convert.FromBase64String(privateKey);

        using RSA rsa = RSA.Create();

        rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

        var securityKey = new RsaSecurityKey(rsa);

        securityKey.CryptoProviderFactory.CacheSignatureProviders = false;


        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.RsaSha256
           );

        var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub,CallerId),

        new Claim("client_type","service")
        
        };

        var token = new JwtSecurityToken(
            issuer : _configuration["Jwt:Issuer"],
            audience : _configuration["Jwt:Audience"],
            claims:claims,
               expires: DateTime.UtcNow.AddMinutes(15),
               signingCredentials: credentials

            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}