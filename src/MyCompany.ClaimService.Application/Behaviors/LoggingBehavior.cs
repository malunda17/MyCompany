using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCompany.Common.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IRabbitLogger _rabbitLogger;
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest,TResponse>> logger, IRabbitLogger rabbitLogger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _rabbitLogger = rabbitLogger ?? throw new ArgumentNullException(nameof(rabbitLogger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            var response = await next();
            _logger.LogInformation($"Handled {typeof(TRequest).Name}");

            var userName = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";

            await _rabbitLogger.LogAsync(new LogMessage(0,userName,$"{typeof(TRequest).Name}"));

            return response;
        }
    }
}