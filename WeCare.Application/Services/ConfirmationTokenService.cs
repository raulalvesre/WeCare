using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class ConfirmationTokenService
{
    private readonly ConfirmationTokenRepository _confirmationTokenRepository;

    public ConfirmationTokenService(ConfirmationTokenRepository confirmationTokenRepository)
    {
        _confirmationTokenRepository = confirmationTokenRepository;
    }

    public async Task<ConfirmationToken> GetByToken(string token)
    {
        var confirmationToken = await _confirmationTokenRepository.GetByToken(token);
        if (confirmationToken is null)
            throw new NotFoundException("Token de confirmação não foi encontrado");

        return confirmationToken;
    }

    public async Task<ConfirmationTokenViewModel> Save(ConfirmationTokenForm form)
    {
        var confirmationToken = new ConfirmationToken
        {
            Token = form.Token,
            UserId = form.UserId
        };

        await _confirmationTokenRepository.Save(confirmationToken);
        
        return new ConfirmationTokenViewModel(confirmationToken);
    }
    
}