using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories;

using WeCare.Infrastructure;

namespace WeCare.Application.Services;

public class AuthService
{
    private readonly UserService _userService;
    private readonly IConfiguration _config;
    
    public AuthService(UserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }

    public async Task<TokenViewModel> AuthenticateUser(LoginRequest loginRequest)
    {
        var user = await _userService.getByEmail(loginRequest.Email);
        if (user is null)
            throw new UnauthorizedException("Usuário ou senha inválidos");

        if (!LoginRequestPasswordAndUserModelPasswordAreTheSame(loginRequest, user))
            throw new UnauthorizedException("Usuário ou senha inválidos");

        return GenerateJWTForUser(user);
    }
    
    private bool LoginRequestPasswordAndUserModelPasswordAreTheSame(LoginRequest req, UserCompleteViewModel userCompleteViewModel)
    {
        return StringCipher.Decrypt(userCompleteViewModel.Password).Equals(req.Password);
    }

    private TokenViewModel GenerateJWTForUser(UserCompleteViewModel user)
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
    
    
    
}