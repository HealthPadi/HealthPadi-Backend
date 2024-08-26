using HealthPadiWebApi.Data;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthPadiWebApi.Repositories.Implementations
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        protected DbSet<Report> _dbSet;
        public ReportRepository(HealthPadiDataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Report>();
        }

        public async Task<IEnumerable<Report>> GetReportsByLocation(string location)
        {
            return await _dbSet.Where(x => x.Location.ToLower() == location.ToLower()).ToListAsync();
        }
    }
}