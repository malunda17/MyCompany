using MediatR;
using MyCompany.LogService.Application.Models;

namespace MyCompany.LogService.Application.Queries
{
    public class GetLogQuery : IRequest<LogViewModel>
    {
        public int LogId { get; private set; }
        public GetLogQuery(int logId)
        {
            LogId = logId;
        }
    }
}