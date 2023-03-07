using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Application.Mappers;

public class OpportunityCauseMapper
{

    public static IEnumerable<OpportunityCause> ToModels(IEnumerable<string> causeNames)
    {
        return causeNames
            .Select(Enumeration.FromDisplayName<OpportunityCause>)
            .ToList();
    }

}