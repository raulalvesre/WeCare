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

    [HttpPost("register-candidate")]
    public async ValueTask<ActionResult> RegisterCandidate(CandidateForm form)
    {
        await _authService.RegisterCandidate(form);
        return NoContent();
    }

    [HttpPost("register-institution")]
    public async ValueTask<ActionResult> RegisterInstitution(InstitutionForm form)
    {
        await _authService.RegisterInstitution(form);
        return NoContent();
    }

    [HttpGet("activate-account")]
    public async ValueTask<ActionResult> ConfirmEmail([FromQuery(Name = "token")] string confirmationToken)
    {
        await _authService.ConfirmEmail(confirmationToken);
        return NoContent();
    }

    [HttpPost("restore-password")]
    public async ValueTask<ActionResult> RestoreUserPassword()
    {
        return NoContent();
    }

    [HttpGet("is-email-registered")]
    public async ValueTask<ActionResult<bool>> IsEmailAlreadyRegistered([FromQuery(Name = "email")] string email)
    {
        
        return Ok(await _authService.IsEmailAlreadyRegistered(email));
    }
    
    [HttpGet("is-telephone-registered")]
    public async ValueTask<ActionResult<bool>> IsTelephoneAlreadyRegistered([FromQuery(Name = "telephone")] string telephone)
    {
        
        return Ok(await _authService.IsTelephoneAlreadyRegistered(telephone));
    }
    
    [HttpGet("is-cpf-registered")]
    public async ValueTask<ActionResult<bool>> IsCpfAlreadyRegistered([FromQuery(Name = "cpf")] string cpf)
    {
        
        return Ok(await _authService.IsCpfAlreadyRegistered(cpf));
    }
    
    [HttpGet("is-cnpj-registered")]
    public async ValueTask<ActionResult<bool>> IsCnpjAlreadyRegistered([FromQuery(Name = "cnpj")] string cnpj)
    {
        
        return Ok(await _authService.IsCnpjAlreadyRegistered(cnpj));
    }

}