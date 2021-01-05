using Microsoft.EntityFrameworkCore;
using MyCompany.ClaimService.Domain;
using MyCompany.ClaimService.Infrastructure.Data.EntityTypeConfiguration;

namespace MyCompany.ClaimService.Infrastructure.Data
{
    public class ClaimServiceDbContext : DbContext
    {
        public DbSet<Claim> Claims { get; set; }
        public ClaimServiceDbContext(DbContextOptions<ClaimServiceDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClaimEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}