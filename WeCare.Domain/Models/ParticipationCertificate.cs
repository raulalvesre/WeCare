namespace WeCare.Domain;

public class ParticipationCertificate
{
    public int Id { get; set; }
    public DateOnly CreationDate { get; set; }

    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }

    public int VolunteerOpportunityId { get; set; }
    public VolunteerOpportunity VolunteerOpportunity { get; set; }
}