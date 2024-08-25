

using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IReportRepository Report { get; }
        IGenericRepository<TaskExecutionLog> TaskExecutionLogger{ get; }
        Task CompleteAsync();
    }
}
