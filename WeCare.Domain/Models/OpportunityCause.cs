using WeCare.Domain.Core;

namespace WeCare.Domain.Models;

public class OpportunityCause 
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string PrimaryColorCode { get; set; }
    public string SecondaryColorCode { get; set; }

    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; } = new List<VolunteerOpportunity>();
    public IEnumerable<Candidate> CandidatesInterestedIn { get; set; } = new List<Candidate>();
}