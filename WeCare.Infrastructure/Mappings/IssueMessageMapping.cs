using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class IssueMessageMapping : IEntityTypeConfiguration<IssueMessage>
{
    public void Configure(EntityTypeBuilder<IssueMessage> builder)
    {
        throw new NotImplementedException();
    }
}