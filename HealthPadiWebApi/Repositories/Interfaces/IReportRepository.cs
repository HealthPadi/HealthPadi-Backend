using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Repositories.Interfaces
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        Task<Report> UpdateReport(Guid id, Report report);
    }
}