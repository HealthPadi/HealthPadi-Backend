using HealthPadiWebApi.DTOs;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAllReportsAsync(string location);
        Task<ReportDto> GetReportByIdAsync(Guid id);
        Task<ReportDto> AddReportAsync(AddReportDto addReportDto);
        Task<ReportDto> DeleteReportAsync(Guid id);
    }
}