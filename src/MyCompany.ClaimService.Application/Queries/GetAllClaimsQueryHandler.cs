using MediatR;
using MyCompany.ClaimService.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Application.Queries
{
    public class GetAllClaimsQueryHandler : IRequestHandler<GetAllClaimsQuery, IEnumerable<ClaimDTO>>
    {
        private readonly IClaimRepository _claimRepository;

        public GetAllClaimsQueryHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<IEnumerable<ClaimDTO>> Handle(GetAllClaimsQuery command, CancellationToken cancellationToken)
        {
            var claims = await _claimRepository.GetAllAsync();
            return claims.Select(claim => new ClaimDTO
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
            });
        }
    }
}