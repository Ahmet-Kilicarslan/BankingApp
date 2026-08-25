using AuthApi.Services;
using Microsoft.AspNetCore.Mvc;
using AuthApi.Models;

namespace AuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{

    private readonly TokenService _tokenService;

    public TokenController(TokenService tokenService)
    {
        _tokenService = tokenService;


    }


    [HttpPost]
    public IActionResult GenerateToken([FromBody]TokenRequestDto request)
    {

        var token = _tokenService.GenerateToken(request.CallerId);
        return Ok(new  { token });



    }




}