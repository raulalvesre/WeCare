using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Domain.Models;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly UnitOfWork _unitOfWork;

    public UserService(UserRepository userRepository, UnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
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

    public async Task ChangePassword(User user, string password)
    {
        user.Password = password;
        await _userRepository.Update(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task SetUserEnabled(long id, bool enabled)
    {
        var user = await _userRepository.GetById(id);

        if (user is null)
            throw new NotFoundException("Usuário não encontrado");

        user.Enabled = enabled;

        await _userRepository.Update(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task<bool> IsEmailAlreadyRegistered(string email)
    {
        return await _userRepository.ExistsByIdNotAndEmail(0, email);
    }

    public async Task<bool> IsTelephoneAlreadyRegistered(string telephone)
    {
        return await _userRepository.ExistsByIdNotAndTelephone(0, telephone);
    }
    
}