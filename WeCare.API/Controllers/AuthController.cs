using Microsoft.AspNetCore.Mvc;
using WeCare.API.Auth;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async ValueTask<ActionResult<TokenViewModel>> Login(LoginRequest loginRequest)
    {
        return Ok(await _authService.AuthenticateUser(loginRequest));
    } 
    
}