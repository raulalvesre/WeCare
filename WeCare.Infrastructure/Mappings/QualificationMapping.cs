// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using WeCare.Domain;
//
// namespace WeCare.Infrastructure.Mappings;
//
// public class QualificationMapping : IEntityTypeConfiguration<Qualification>
// {
//     public void Configure(EntityTypeBuilder<Qualification> builder)
//     {
//         builder.ToTable("qualifications");
//
//         builder.HasKey(x => x.Id);
//
//         builder.Property(x => x.Name)
//             .IsRequired();
//     }
// }