using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Feed> Feed { get; }

        IReportRepository Report { get; }
        Task CompleteAsync();
    }
}
