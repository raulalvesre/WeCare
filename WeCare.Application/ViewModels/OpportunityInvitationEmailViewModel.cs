namespace WeCare.Application.ViewModels;

public class OpportunityInvitationEmailViewModel
{
    public long Id { get; set; }
    public string InvitationMessage { get; set; }
    public VolunteerOpportunityViewModel Opportunity { get; set; }
    public InstitutionViewModel InstitutionViewModel { get; set; }
}