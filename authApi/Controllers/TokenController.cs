using AuthApi.Models;
using AuthApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace AuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{

    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;
    public TokenController(TokenService tokenService,IConfiguration configuration)
    {
        _tokenService = tokenService;
        _configuration= configuration;

    }


    [HttpPost]
    public IActionResult GenerateToken([FromBody]TokenRequestDto request)
    {

        var validSecret = _configuration[$"OAuthClients:{request.CallerId}"];


        if (validSecret == null || !CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(validSecret),
                Encoding.UTF8.GetBytes(request.ClientSecret)))
        {
            return Unauthorized();
        }
        var token = _tokenService.GenerateToken(request.CallerId);


        return Ok(new  { token });



    }




}