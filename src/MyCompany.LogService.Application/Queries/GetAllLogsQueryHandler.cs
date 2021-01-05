using MediatR;
using MyCompany.LogService.Application.Models;
using MyCompany.LogService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.LogService.Application.Queries
{
    public class GetAllLogsQueryHandler : IRequestHandler<GetAllLogsQuery,IEnumerable<LogViewModel>>
    {
        private readonly ILogRepository _logRepository;
        public GetAllLogsQueryHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
        }
        public async Task<IEnumerable<LogViewModel>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await _logRepository.GetAllAsync();

            return logs.Select(log => new LogViewModel
            { 
                LogId = log.LogId,
                UserId = log.UserId,
                UserName = log.UserName,
                ActionPerformed = log.ActionPerformed
            });
        }
    }
}