using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.LogService.Application.Behaviors;
using System.Reflection;

namespace MyCompany.LogService.Application
{
    public static class Extensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(LoggingBehavior<,>));
        }
    }
}