using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasDiscriminator<string>("user_type")
            .HasValue<Candidate>("CANDIDATE")
            .HasValue<Institution>("INSTITUTION");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(x => x.Password)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();
        
        builder.Property(x => x.Telephone)
            .IsRequired();

        builder.Property(x => x.Address)
            .IsRequired();
        
        builder.Property(x => x.Number)
            .IsRequired();

        builder.Property(x => x.Complement);
        
        builder.Property(x => x.City)
            .IsRequired();
        
        builder.Property(x => x.City)
            .IsRequired();

        builder.Property(x => x.Neighborhood);
        
        builder.Property(x => x.State)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(x => x.Cep)
            .IsRequired();
        
        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LastUpdateDate);
    }
}