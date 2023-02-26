using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WeCare.Application.Exceptions;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure;

namespace WeCare.API.Auth;

public class AuthService
{
    private readonly UserService _userService;
    private readonly CandidateService _candidateService;
    private readonly InstitutionService _institutionService;
    private readonly EmailService _emailService;
    private readonly ConfirmationTokenService _confirmationTokenService;
    private readonly IConfiguration _config;
    
    public AuthService(UserService userService,
        IConfiguration config,
        CandidateService candidateService,
        EmailService emailService, 
        ConfirmationTokenService confirmationTokenService, 
        InstitutionService institutionService)
    {
        _userService = userService;
        _config = config;
        _candidateService = candidateService;
        _emailService = emailService;
        _confirmationTokenService = confirmationTokenService;
        _institutionService = institutionService;
    }

    public async Task<TokenViewModel> AuthenticateUser(LoginRequest loginRequest)
    {
        var user = await _userService.GetByEmail(loginRequest.Email);
        if (user is null)
            throw new UnauthorizedException("Usuário ou senha inválidos");

        if (!LoginRequestPasswordAndUserModelPasswordAreTheSame(loginRequest, user))
            throw new UnauthorizedException("Usuário ou senha inválidos");

        return GenerateJwt(user);
    }
    
    private bool LoginRequestPasswordAndUserModelPasswordAreTheSame(LoginRequest req, UserCompleteViewModel userCompleteViewModel)
    {
        return StringCipher.Decrypt(userCompleteViewModel.Password).Equals(req.Password);
    }

    private TokenViewModel GenerateJwt(UserCompleteViewModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("jwt-secret"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Type)
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenViewModel
        {
            Token = tokenHandler.WriteToken(token)
        };
    }

    public async Task RegisterCandidate(CandidateForm form)
    {
        var candidateViewModel = await _candidateService.Save(form);

        var token = Guid.NewGuid().ToString();
        await _confirmationTokenService.Save(new ConfirmationTokenForm(token, candidateViewModel.Id));
        await SendAccountConfirmationEmail(form.Email, token);
    }
    
    private async Task SendAccountConfirmationEmail(string email, string confirmationToken)
    {
        var emailRequest = new EmailRequest
        {
            ToEmail = email,
            Subject = "WeCare - Ativação de conta",
            Body = $"<a href='http://localhost:5098/api/auth/activateAccount?token={confirmationToken}'>Clique aqui para ativar sua conta.</a>"
        };
        
        // await _emailService.SendEmailAsync(emailRequest);
    }
    
    public async Task RegisterInstitution(InstitutionForm form)
    {
        var candidateViewModel = await _institutionService.Save(form);

        var token = Guid.NewGuid().ToString();
        await _confirmationTokenService.Save(new ConfirmationTokenForm(token, candidateViewModel.Id));
        // await SendAccountConfirmationEmail(form.Email, token);
    }
    
    public async Task ConfirmEmail(string token)
    {
        var confirmationToken = await _confirmationTokenService.GetByToken(token);
        if (DateTime.Now.Subtract(confirmationToken.CreationDate).TotalHours > 2)
            throw new GoneException("Token de confirmação expirado");

        // await _userService.SetUserEnabled(confirmationToken.UserId, true);
    }
    
    public async Task RestoreUserPassword(long userId)
    {
        var user = await _userService.GetById(userId);
        if (user is null)
            throw new NotFoundException("Candidato não encontrado");
        
        var emailRequest = new EmailRequest
        {
            ToEmail = user.Email,
            Subject = "WeCare - Recuperação de senha",
            Body = "<a href='http://localhost:5098/api/auth/login'>Clique aqui para recuperar sua senha.</a>"
        };
    }
    
    private async Task SendPasswordRecoveryEmail(string email, string confirmationToken)
    {
        var emailRequest = new EmailRequest
        {
            ToEmail = email,
            Subject = "WeCare - Recuperação de senha",
            Body = $"<a href='http://localhost:5098/api/auth/login'>Clique aqui para recuperar sua senha.</a>"
        };
        
        // await _emailService.SendEmailAsync(emailRequest);
    }

    public async Task<bool> IsEmailAlreadyRegistered(string email)
    {
        return await _userService.IsEmailAlreadyRegistered(email);
    }

    public async Task<bool> IsTelephoneAlreadyRegistered(string telephone)
    {
        return await _userService.IsTelephoneAlreadyRegistered(telephone);

    }
    
    public async Task<bool> IsCpfAlreadyRegistered(string cpf)
    {
        return await _candidateService.IsCpfAlreadyRegistered(cpf);
    }
    
    public async Task<bool> IsCnpjAlreadyRegistered(string cnpj)
    {
        return await _institutionService.IsCnpjAlreadyRegistered(cnpj);
    }
    
}