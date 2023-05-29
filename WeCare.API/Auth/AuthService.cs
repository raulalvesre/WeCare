using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WeCare.Application.EmailTemplates;
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

    public async Task<TokenViewModel> AuthenticateCandidate(LoginRequest loginRequest)
    {
        var candidate = await _candidateService.GetByEmailAndEnabled(loginRequest.Email);
        if (candidate is null)
            throw new UnauthorizedException("Usuário ou senha inválidos");

        if (!LoginRequestPasswordAndUserModelPasswordAreTheSame(loginRequest, candidate))
            throw new UnauthorizedException("Usuário ou senha inválidos");

        return GenerateJwt(candidate);
    }
    
    public async Task<TokenViewModel> AuthenticateInstitution(LoginRequest loginRequest)
    {
        var institution = await _institutionService.GetByEmailAndEnabled(loginRequest.Email);
        if (institution is null)
            throw new UnauthorizedException("Usuário ou senha inválidos");

        if (!LoginRequestPasswordAndUserModelPasswordAreTheSame(loginRequest, institution))
            throw new UnauthorizedException("Usuário ou senha inválidos");

        return GenerateJwt(institution);
    }
    
    public async Task<TokenViewModel> AuthenticateAdmin(LoginRequest loginRequest)
    {
        var admin = await _userService.GetByEmailAndEnabled(loginRequest.Email);

        if (!LoginRequestPasswordAndUserModelPasswordAreTheSame(loginRequest, admin))
            throw new UnauthorizedException("Usuário ou senha inválidos");

        return GenerateJwt(admin);
    }
    
    private bool LoginRequestPasswordAndUserModelPasswordAreTheSame(LoginRequest req, UserCompleteViewModel userCompleteViewModel)
    {
        return StringCipher.Decrypt(userCompleteViewModel.Password).Equals(req.Password);
    }

    private TokenViewModel GenerateJwt(UserCompleteViewModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JWT_SECRET"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
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

    public async Task RegisterCandidate(CandidateCreateForm createForm)
    {
        var candidateViewModel = await _candidateService.Save(createForm);

        var token = Guid.NewGuid().ToString();
        await _confirmationTokenService.Save(new ConfirmationTokenForm(token, candidateViewModel.Id));
        await SendAccountActivationEmail(candidateViewModel.Name, createForm.Email, token);
    }
    
    private async Task SendAccountActivationEmail(string name, string email, string confirmationToken)
    {
        var subject = "WeCare - Ativação de conta";

        var emailConfirmationViewModel = new EmailConfirmationViewModel
        {
            UserName = name,
            ConfirmationToken = confirmationToken,
            Url = Environment.GetEnvironmentVariable("FRONT_URL") ?? "http://locahost:4200"
        };
        
        await _emailService.SendEmailAsync(email, subject, nameof(AccountActivation), emailConfirmationViewModel);
    }
    
    public async Task RegisterInstitution(InstitutionCreateForm createForm)
    {
        var institutionViewModel = await _institutionService.Save(createForm);

        var token = Guid.NewGuid().ToString();
        await _confirmationTokenService.Save(new ConfirmationTokenForm(token, institutionViewModel.Id));
        await SendAccountActivationEmail(institutionViewModel.Email, createForm.Email, token);
    }
    
    public async Task ActivateAccount(string token)
    {
        var confirmationToken = await _confirmationTokenService.GetByToken(token);
        if (DateTime.Now.Subtract(confirmationToken.CreationDate).TotalHours > 2)
            throw new GoneException("Token de confirmação expirado");

        await _userService.SetUserEnabled(confirmationToken.UserId, true);
    }
    
    public async Task SendPasswordRecoveryEmail(long userId)
    {
        var user = await _userService.GetById(userId);
        if (user is null)
            throw new NotFoundException("Usuário não encontrado");
        
        var token = Guid.NewGuid().ToString();
        await _confirmationTokenService.Save(new(token, userId));
        
        const string subject = "WeCare - Recuperação de senha";
        var passwordRecoveryViewModel = new PasswordRecoveryViewModel
        {
            CandidateName = user.Name,
            ConfirmationToken = token,
            Url = Environment.GetEnvironmentVariable("FRONT_URL") ?? "http://locahost:4200"
        };
        
        await _emailService.SendEmailAsync(user.Email, subject, nameof(PasswordRecovery), passwordRecoveryViewModel);
    }

    public async Task RecoverUserPassword(PasswordRecoveryForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);
        
        var token = await _confirmationTokenService.GetByToken(form.Token);
        if (token is null)
            throw new NotFoundException("Token não encontrado");

        await _userService.ChangePassword(token.User, form.Password);
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