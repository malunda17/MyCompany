using Microsoft.EntityFrameworkCore;
using MyComapany.LogService.Infrastructure.Data.EntityTypeConfiguration;
using MyCompany.LogService.Domain;

namespace MyComapany.LogService.Infrastructure.Data
{
    public class LogServiceDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public LogServiceDbContext(DbContextOptions<LogServiceDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
