using MediatR;
using MyCompany.ClaimService.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Application.Queries
{
    public class GetClaimQueryHandler : IRequestHandler<GetClaimQuery, ClaimDTO>
    {
        private readonly IClaimRepository _claimRepository;

        public GetClaimQueryHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<ClaimDTO> Handle(GetClaimQuery query, CancellationToken cancellationToken)
        {
            var claim = await _claimRepository.GetAsync(query.ClaimId);
            if (claim == null)
            {
                return null;
            }
            return new ClaimDTO
            {
                ClaimId = claim.ClaimId,
                Date = claim.Date,
                UserId = claim.UserId,
                Description = claim.Description,
                Incidence = claim.Incidence,
                DamagedItem = claim.DamagedItem,
                Status = nameof(claim.Status),
                Street = claim.Street,
                City = claim.City,
                Country = claim.Country
            };
        }
    }
}