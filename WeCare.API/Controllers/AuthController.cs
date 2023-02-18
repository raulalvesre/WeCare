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

    [HttpPost("candidate/register")]
    public async ValueTask<ActionResult> RegisterCandidate(CandidateForm form)
    {
        await _authService.RegisterCandidate(form);
        return NoContent();
    }

    [HttpPost("institution/register")]
    public async ValueTask<ActionResult> RegisterInstitution(CandidateForm form)
    {
        await _authService.RegisterCandidate(form);
        return NoContent();
    }

    [HttpGet("activateAccount")]
    public async ValueTask<ActionResult> ConfirmEmail([FromQuery(Name = "token")] string confirmationToken)
    {
        await _authService.ConfirmEmail(confirmationToken);
        return NoContent();
    }

    [HttpPost("restorePassword")]
    public async ValueTask<ActionResult> RestoreUserPassword()
    {
        return NoContent();
    }
    
}