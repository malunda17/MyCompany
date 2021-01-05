using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyComapany.LogService.Infrastructure.Data;
using MyComapany.LogService.Infrastructure.RabbitMQ;
using MyComapany.LogService.Infrastructure.Repositories;
using MyCompany.Common.Logger;
using MyCompany.LogService.Domain;

namespace MyComapany.LogService.Infrastructure
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

                
                services.Configure<RabbitConfig>(configuration.GetSection("Rabbit"));
                services.AddSingleton<IRabbitConfig>(sp =>
                   sp.GetRequiredService<IOptions<RabbitConfig>>().Value);


            }

            services.AddDbContext<LogServiceDbContext>(options=> options.UseSqlServer(connectionString,options => options.EnableRetryOnFailure()),ServiceLifetime.Singleton);
            //services.AddDbContext<LogServiceDbContext>(options => options.UseInMemoryDatabase(databaseName:"db"),ServiceLifetime.Singleton);
            services.AddTransient<ILogRepository, LogRepository>();
        }
    }
}