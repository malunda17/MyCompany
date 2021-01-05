using MediatR;
using System.Collections.Generic;

namespace MyCompany.ClaimService.Application.Queries
{
    public class GetAllClaimsQuery : IRequest<IEnumerable<ClaimDTO>>
    {
    }
}