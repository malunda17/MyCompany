using MediatR;
using MyCompany.LogService.Application.Models;
using System.Collections.Generic;

namespace MyCompany.LogService.Application.Queries
{
    public class GetAllLogsQuery : IRequest<IEnumerable<LogViewModel>>
    {
    }
}
