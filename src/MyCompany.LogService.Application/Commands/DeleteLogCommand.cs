using MediatR;
using System.Runtime.Serialization;

namespace MyCompany.LogService.Application.Commands
{
    [DataContract]
    public class DeleteLogCommand : IRequest<bool>
    {
        public int LogId { get; private set; }
        public DeleteLogCommand(int logId)
        {
            LogId = logId;
        }
    }
}