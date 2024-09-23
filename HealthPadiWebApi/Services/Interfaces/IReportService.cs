using HealthPadiWebApi.DTOs.Request;
using HealthPadiWebApi.DTOs.Response;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IReportService
    {
        Task<(bool isSuccess, string? message, ReportDto report)> AddReportAsync(AddReportDto addReportDto, Guid userId);
        Task<List<ReportDto>> GetAllReportsAsync(string location);
        Task<ReportDto> GetReportByIdAsync(Guid id);
        Task<ReportDto> DeleteReportAsync(Guid id);
    }
}