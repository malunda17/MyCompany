using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyComapany.LogService.Infrastructure;
using MyComapany.LogService.Infrastructure.Data;
using MyCompany.LogService.BackgroundTasks.Tasks;

namespace MyCompany.LogService.BackgroundTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LogServiceDbContext>();
                dbContext.Database.EnsureCreated();

                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddInfrastructure();
                    services.AddHostedService<LogsTask>();
                });
    }
}