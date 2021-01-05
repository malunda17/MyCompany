using MediatR;

namespace MyCompany.ClaimService.Application.Commands
{
    public class DeleteClaimCommand : IRequest<bool>
    {
        public int ClaimId { get;  private set; }
        public DeleteClaimCommand(int claimId)
        {
            ClaimId = claimId;
        }
    }
}