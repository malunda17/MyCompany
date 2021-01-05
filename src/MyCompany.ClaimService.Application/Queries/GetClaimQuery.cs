using MediatR;

namespace MyCompany.ClaimService.Application.Queries
{
    public class GetClaimQuery : IRequest<ClaimDTO>
    {
        public int ClaimId { get; private set; }
        public GetClaimQuery(int claimId)
        {
            ClaimId = claimId;
        }
    }
}