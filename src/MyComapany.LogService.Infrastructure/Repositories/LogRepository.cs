using Microsoft.EntityFrameworkCore;
using MyComapany.LogService.Infrastructure.Data;
using MyCompany.LogService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyComapany.LogService.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly LogServiceDbContext _context;
        private readonly DbSet<Log> _logs;

        public LogRepository(LogServiceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logs = _context.Set<Log>();
        }
        public async Task<bool> AddAsync(Log log)
        {
            await _logs.AddAsync(log);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteAsync(int logId)
        {
            var logToDelete = await _logs.FindAsync(logId);
            if (logToDelete == null)
            {
                return false;
            }

            _logs.Remove(logToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Log>> GetAllAsync(Expression<Func<Log, bool>> filter = null)
        {
            IQueryable<Log> query = _logs;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();

        }

        public async Task<Log> GetAsync(int logId)
        {
            return await _logs.FindAsync(logId);
        }
    }
}
