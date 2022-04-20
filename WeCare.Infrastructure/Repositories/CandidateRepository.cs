using WeCare.Domain;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class CandidateRepository : GenericRepository<Candidate>
{
    public CandidateRepository(DatabaseContext database) : base(database) { }
}