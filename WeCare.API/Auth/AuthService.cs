using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories;

using WeCare.Infrastructure;

namespace WeCare.Application.Services;

public class AuthService
{
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<TokenViewModel>GenerateJWTForUser(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetByEmail(loginRequest.Email);
        if (user is null)
            throw new UnauthorizedException("Usuário ou senha inválidos");

        if (!LoginRequestPasswordAndUserModelPasswordAreTheSame(loginRequest, user))
            throw new UnauthorizedException("Usuário ou senha inválidos");
        
        return null;
    }
    
    private bool LoginRequestPasswordAndUserModelPasswordAreTheSame(LoginRequest req, User model)
    {
        return StringCipher.Decrypt(model.Password).Equals(req.Password);
    }
    
}