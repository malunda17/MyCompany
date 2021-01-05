using MediatR;
using MyCompany.ClaimService.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Application.Commands
{
    public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand, bool>
    {
        private readonly IClaimRepository _claimRepository;
        public DeleteClaimCommandHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }
        public async Task<bool> Handle(DeleteClaimCommand command, CancellationToken cancellationToken)
        {
            return await _claimRepository.DeleteAsync(command.ClaimId);
        }
    }
}