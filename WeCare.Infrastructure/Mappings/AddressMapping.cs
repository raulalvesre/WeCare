using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Street);
        builder.Property(x => x.Number);
        builder.Property(x => x.Complement);
        builder.Property(x => x.City);
        builder.Property(x => x.Neighborhood);
        builder.Property(x => x.PostalCode);
    }
}