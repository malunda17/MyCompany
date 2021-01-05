using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.ClaimService.Domain;
using MyCompany.ClaimService.Infrastructure.Data;
using MyCompany.ClaimService.Infrastructure.Repositories;
using MyCompany.Common.Logger;

namespace MyCompany.ClaimService.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            var connectionString = "";
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                connectionString = serviceProvider.GetService<IConfiguration>().GetConnectionString("db");
            }
            services.AddDbContext<ClaimServiceDbContext>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddSingleton<IRabbitLogger, RabbitLogger>();
        }
    }
}