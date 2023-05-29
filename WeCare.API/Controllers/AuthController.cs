using Microsoft.AspNetCore.Mvc;
using WeCare.API.Auth;
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
    
    [HttpPost("login-candidate")]
    public async ValueTask<ActionResult<TokenViewModel>> LoginCandidate(LoginRequest loginRequest)
    {
        return Ok(await _authService.AuthenticateCandidate(loginRequest));
    }
    
    [HttpPost("login-institution")]
    public async ValueTask<ActionResult<TokenViewModel>> LoginInstitution(LoginRequest loginRequest)
    {
        return Ok(await _authService.AuthenticateInstitution(loginRequest));
    }
    
    [HttpPost("login-admin")]
    public async ValueTask<ActionResult<TokenViewModel>> LoginAdmin(LoginRequest loginRequest)
    {
        return Ok(await _authService.AuthenticateAdmin(loginRequest));
    }

    [HttpPost("register-candidate")]
    public async ValueTask<ActionResult> RegisterCandidate(CandidateCreateForm createForm)
    {
        await _authService.RegisterCandidate(createForm);
        return NoContent();
    }

    [HttpPost("register-institution")]
    public async ValueTask<ActionResult> RegisterInstitution(InstitutionCreateForm createForm)
    {
        await _authService.RegisterInstitution(createForm);
        return NoContent();
    }

    [HttpGet("activate-account")]
    public async ValueTask<ActionResult> ActivateAccount([FromQuery(Name = "token")] string confirmationToken)
    {
        await _authService.ActivateAccount(confirmationToken);
        return NoContent();
    }
    
    [HttpPost("password-recovery/email/{email}")]
    public async ValueTask<ActionResult> SendPasswordRecoveryEmail(string email)
    {
        await _authService.SendPasswordRecoveryEmail(email);
        return NoContent();
    }

    [HttpPost("password-recovery")]
    public async ValueTask<ActionResult> RecoverPassword(PasswordRecoveryForm form)
    {
        await _authService.RecoverUserPassword(form);
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