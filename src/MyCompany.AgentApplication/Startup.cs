using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyCompany.AgentApplication.Configuration;
using MyCompany.AgentApplication.Services;
using System.IdentityModel.Tokens.Jwt;

namespace MyCompany.AgentApplication
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
            services.Configure<ClaimServiceApiConfig>(Configuration.GetSection("ClaimServiceApi"));
            
            services.AddSingleton<IClaimServiceApiConfig>(sp =>
                sp.GetRequiredService<IOptions<ClaimServiceApiConfig>>().Value);

            services.AddHttpClient<IClaimService, ClaimService>();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddAuthentication(options => 
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc",options=> 
                {
                    options.Authority = Configuration.GetSection("IdentityServer")["BaseAddress"];
                    options.ClientId= Configuration.GetSection("IdentityServer")["ClientId"];
                    options.ClientSecret = Configuration.GetSection("IdentityServer")["ClientSecret"];
                    options.ResponseType = "code";
                    options.SaveTokens = true;
                    options.Scope.Add("ClaimServiceApi");
                    options.Scope.Add("offline_access");

                });

            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
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