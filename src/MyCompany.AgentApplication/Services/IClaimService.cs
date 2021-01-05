using MyCompany.AgentApplication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.AgentApplication.Services
{
    public interface IClaimService
    {
        Task<ClaimDTO> AddAsync(ClaimDTO claim);

        Task<bool> DeleteAsync(int claimId);

        Task<bool> UpdateAsync(ClaimDTO claim);

        Task<ClaimDTO> GetAsync(int claimId);

        Task<IEnumerable<ClaimDTO>> GetAllClaims(string keyword = null);
    }
}