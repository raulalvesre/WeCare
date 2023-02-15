using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserCompleteViewModel> GetById(long id)
    {
        var user = await _userRepository.GetById(id);

        if (user is null)
            throw new NotFoundException("Usuário não encontrado");

        return new UserCompleteViewModel(user);
    }


    public async Task<UserCompleteViewModel> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);

        if (user is null)
            throw new NotFoundException("Usuário não encontrado");

        return new UserCompleteViewModel(user);
    }

    public async Task SetUserEnabled(long id, bool enabled)
    {
        var user = await _userRepository.GetById(id);

        if (user is null)
            throw new NotFoundException("Usuário não encontrado");

        user.Enabled = enabled;

        await _userRepository.Save(user);
    }
    
}