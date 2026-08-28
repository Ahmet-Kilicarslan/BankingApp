namespace AuthApi.Models;

public class TokenRequestDto
{
    public required string CallerId { get; set; }
    public required string ClientSecret { get; set; }
}