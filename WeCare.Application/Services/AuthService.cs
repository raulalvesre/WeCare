using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories;

using WeCare.Infrastructure;

namespace WeCare.Application.Services;

public class AuthService
{
    private readonly CandidateRepository _candidateRepository;
    private readonly InstitutionRepository _institutionRepository;

    public AuthService(CandidateRepository candidateRepository, InstitutionRepository institutionRepository)
    {
        _candidateRepository = candidateRepository;
        _institutionRepository = institutionRepository;
    }

    public async Task<TokenViewModel> GetTokenForCandidate(LoginRequest loginRequest)
    {
        var candidate = await _candidateRepository.GetByEmail(loginRequest.Email);
        if (candidate is null)
            throw new UnauthorizedException("Usuário ou senha inválidos");
        
        return null;
    } 
    
    public async Task<TokenViewModel> GetTokenForInstitution(LoginRequest loginRequest)
    {
        var institution = await _institutionRepository.GetByEmail(loginRequest.Email);
        if (institution is null)
            throw new UnauthorizedException("Usuário ou senha inválidos");

        return null;
    } 
    
    private bool LoginRequestPasswordAndCandidateModelPasswordAreTheSame(LoginRequest req, Candidate model)
    {
        return StringCipher.Decrypt(model.Password).Equals(req.Password);
    }
    
    private bool LoginRequestPasswordAndInstitutionModelPasswordAreTheSame(LoginRequest req, Institution model)
    {
        return StringCipher.Decrypt(model.Password).Equals(req.Password);
    }
}