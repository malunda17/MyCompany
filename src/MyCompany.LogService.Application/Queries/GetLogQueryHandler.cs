using MediatR;
using MyCompany.LogService.Application.Models;
using MyCompany.LogService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.LogService.Application.Queries
{
    public class GetLogQueryHandler : IRequestHandler<GetLogQuery, LogViewModel>
    {
        private readonly ILogRepository _logRepository;
        public GetLogQueryHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository)); ;
        }
        public async Task<LogViewModel> Handle(GetLogQuery message, CancellationToken cancellationToken)
        {
            var log = await _logRepository.GetAsync(message.LogId);

            return (log != null) ? new LogViewModel { LogId = log.LogId, UserId = log.UserId, UserName = log.UserName, ActionPerformed = log.ActionPerformed } : null;

        }
    }
}