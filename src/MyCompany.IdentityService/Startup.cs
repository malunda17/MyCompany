using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyCompany.Common.Logger;
using MyCompany.IdentityService.Configuration;
using MyCompany.IdentityService.Data;
using MyCompany.IdentityService.Models;

namespace MyCompany.IdentityService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitConfig>(Configuration.GetSection("Rabbit"));
            services.AddSingleton<IRabbitConfig>(sp =>
                   sp.GetRequiredService<IOptions<RabbitConfig>>().Value);
            services.AddSingleton<IRabbitLogger, RabbitLogger>();

            services.AddControllersWithViews();
            services.AddDbContext<IdentityServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("db"));
            });

            services.AddIdentity<User,IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<IdentityServiceDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options=>
            {
                options.Cookie.Name = "IdentityService.Cookie";
                options.LoginPath = "/Auth/Login";
            });
            

            var builder = services.AddIdentityServer(options =>
         {
             options.Events.RaiseErrorEvents = true;
             options.Events.RaiseInformationEvents = true;
             options.Events.RaiseFailureEvents = true;
             options.Events.RaiseSuccessEvents = true;
            
         })

                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<User>();

            builder.AddDeveloperSigningCredential();
            services.AddAuthentication();

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}