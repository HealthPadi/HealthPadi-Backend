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
        public async Task<Report> UpdateReport(Guid id, Report report)
        {
            var existingReport = await _dbSet.FirstOrDefaultAsync(x => x.ReportId == id);
            if (existingReport == null)
            {
                return null;
            }
            existingReport.Content = report.Content;
            return existingReport;
        }
    }
}