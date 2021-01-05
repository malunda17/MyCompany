using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.LogService.Domain;

namespace MyComapany.LogService.Infrastructure.Data.EntityTypeConfiguration
{
    public class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("t_logs");
            builder.HasKey(l =>l.LogId) ;
            builder.Property(l=>l.UserId).IsRequired();
            builder.Property(l=>l.UserName).IsRequired();
            builder.Property(l=>l.ActionPerformed).IsRequired();
            builder.Property(l=>l.Timestamp).IsRequired();

           
        }
    }
}