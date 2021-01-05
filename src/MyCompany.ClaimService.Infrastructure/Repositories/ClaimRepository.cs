using Microsoft.EntityFrameworkCore;
using MyCompany.ClaimService.Domain;
using MyCompany.ClaimService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Infrastructure.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ClaimServiceDbContext _context;
        private readonly DbSet<Claim> _claims;

        public ClaimRepository(ClaimServiceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _claims = _context.Set<Claim>();
        }

        public async Task<bool> AddAsync(Claim claim)
        {
            await _claims.AddAsync(claim);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int claimId)
        {
            var entityToDelete = await _claims.FindAsync(claimId);
            if (entityToDelete == null)
            {
                return false;
            }
            _claims.Remove(entityToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Claim>> GetAllAsync(Expression<Func<Claim, bool>> filter = null)
        {
            IQueryable<Claim> query = _claims;
            if (filter!=null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<Claim> GetAsync(int claimId)
        {
            return await _claims.FindAsync(claimId);
        }

        public async  Task<bool> UpdateAsync(Claim claimToUpdate)
        {
            _claims.Update(claimToUpdate);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}