using WeCare.Application.ViewModels;
using WeCare.Domain.Models;

namespace WeCare.Application.Mappers;

public class OpportunityCauseMapper
{
    public OpportunityCauseViewModel FromModel(OpportunityCause model)
    {
        return new OpportunityCauseViewModel
        {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
            PrimaryColorCode = model.PrimaryColorCode,
            SecondaryColorCode = model.SecondaryColorCode
        };
    }   
}