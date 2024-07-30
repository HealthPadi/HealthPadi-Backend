using HealthPadiWebApi.Data;
using HealthPadiWebApi.Repositories.Interfaces;

namespace HealthPadiWebApi.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HealthPadiDataContext dbContext;

        public UnitOfWork(HealthPadiDataContext dbContext)
        {
            this.dbContext = dbContext;
            Report = new ReportRepository(dbContext);
        }
        public IReportRepository Report { get; private set; }

        public async Task CompleteAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}