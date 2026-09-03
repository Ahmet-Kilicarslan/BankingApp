namespace AuthApi.Models;

public class TokenRequestDto
{
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}