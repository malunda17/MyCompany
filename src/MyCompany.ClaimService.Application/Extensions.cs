using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.ClaimService.Application.Behaviors;
using System.Reflection;

namespace MyCompany.ClaimService.Application
{
    public static class Extensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }
    }
}