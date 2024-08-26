using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Repositories.Interfaces
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        Task<IEnumerable<Report>> GetReportsByLocation(string location);
    }
}