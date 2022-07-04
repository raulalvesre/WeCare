namespace WeCare.Domain;

public class ParticipationCertificate
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }

    public int CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    public long VolunteerOpportunityId { get; set; }
    public VolunteerOpportunity? VolunteerOpportunity { get; set; }
}