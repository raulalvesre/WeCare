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

    public async Task<UserCompleteViewModel> getByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);

        if (user is null)
            throw new NotFoundException("Usuário não encontrado");

        return new UserCompleteViewModel(user);
    }
    
}