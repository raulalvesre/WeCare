using WeCare.Application.Mappers;
using WeCare.Infrastructure.Mappings;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class InstitutionService
{
    private readonly InstitutionRepository _institutionRepository;
    private readonly UserRepository _userRepository;
    private readonly InstitutionMapper _mapper;

    public InstitutionService(InstitutionRepository institutionRepository, UserRepository userRepository, InstitutionMapper mapper)
    {
        _institutionRepository = institutionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    
    
}