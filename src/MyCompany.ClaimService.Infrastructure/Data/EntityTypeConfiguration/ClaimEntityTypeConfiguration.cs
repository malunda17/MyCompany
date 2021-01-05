using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.ClaimService.Domain;

namespace MyCompany.ClaimService.Infrastructure.Data.EntityTypeConfiguration
{
    public class ClaimEntityTypeConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable("t_claims");
            builder.HasKey(c => c.ClaimId);
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.Date).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Incidence).IsRequired();
            builder.Property(c => c.DamagedItem).IsRequired();
            builder.Property(c => c.Street).IsRequired();
            builder.Property(c => c.City).IsRequired();
            builder.Property(c => c.Country).IsRequired();
        }
    }
}