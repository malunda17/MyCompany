using MediatR;
using MyCompany.ClaimService.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Application.Commands
{
    public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, int>
    {
        private readonly IClaimRepository _claimRepository;
        public CreateClaimCommandHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }
        public async Task<int> Handle(CreateClaimCommand command, CancellationToken cancellationToken)
        {
            var newClaim = new Claim(command.Date,command.UserId,command.Description,command.Incidence,command.DamagedItem,command.Street,command.City,command.Country);
            await _claimRepository.AddAsync(newClaim);
            return newClaim.ClaimId;
        }
    }
}