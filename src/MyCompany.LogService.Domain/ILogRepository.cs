using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyCompany.LogService.Domain
{
    public interface ILogRepository
    {
        Task<bool> DeleteAsync(int logId);
        Task<bool> AddAsync(Log log);
        Task<Log> GetAsync(int logId);
        Task<IEnumerable<Log>> GetAllAsync(Expression<Func<Log, bool>> filter = null);
    }
}