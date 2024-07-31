using HealthPadiWebApi.Data;
using HealthPadiWebApi.Repositories.Interfaces;

namespace HealthPadiWebApi.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HealthPadiDataContext _dbContext;

        public UnitOfWork(HealthPadiDataContext dbContext)
        {
            _dbContext = dbContext;
            Report = new ReportRepository(dbContext);
        }
        public IReportRepository Report { get; private set; }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}