using MediatR;
using MyCompany.LogService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.LogService.Application.Commands
{
    public class DeleteLogCommandHandler : IRequestHandler<DeleteLogCommand, bool>
    {
        private readonly ILogRepository _logRepository;

        public DeleteLogCommandHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
        }

        public async Task<bool> Handle(DeleteLogCommand message, CancellationToken cancellationToken)
        {
            return await _logRepository.DeleteAsync(message.LogId);
        }
    }
}