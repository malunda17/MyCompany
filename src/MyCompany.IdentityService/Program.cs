using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.IdentityService.Data;
using MyCompany.IdentityService.Models;


namespace MyCompany.IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host =  CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var dbContext =  scope.ServiceProvider.GetRequiredService<IdentityServiceDbContext>();
                dbContext.Database.EnsureCreated();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var user = new User
                {
                    UserName = "admin",
                    Name = "Administractor",
                    Surname = "Admin"
                };
                userManager.CreateAsync(user,"admin").GetAwaiter().GetResult();


            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}