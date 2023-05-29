using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class OpportunityCauseViewModel
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string PrimaryColorCode { get; set; }
    public string SecondaryColorCode { get; set; }

    public OpportunityCauseViewModel(OpportunityCause opportunityCause)
    {
        Id = opportunityCause.Id;
        Code = opportunityCause.Code;
        Name = opportunityCause.Name;
        PrimaryColorCode = opportunityCause.PrimaryColorCode;
        SecondaryColorCode = opportunityCause.SecondaryColorCode;
    }
}