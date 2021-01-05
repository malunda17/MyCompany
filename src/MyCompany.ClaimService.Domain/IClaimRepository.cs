using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Domain
{
    public interface IClaimRepository
    {
        Task<bool> AddAsync(Claim claim);
        Task<Claim> GetAsync(int claimId);
        Task<IEnumerable<Claim>> GetAllAsync(Expression<Func<Claim, bool>> filter = null);

        Task<bool> DeleteAsync(int claimId);
        Task<bool> UpdateAsync(Claim claimToUpdate);
    }
}
