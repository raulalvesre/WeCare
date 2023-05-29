using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class QualificationViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }

    public QualificationViewModel(Qualification qualification)
    {
        Id = qualification.Id;
        Name = qualification.Name;
    }
}