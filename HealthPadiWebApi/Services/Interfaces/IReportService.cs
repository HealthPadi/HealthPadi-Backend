using HealthPadiWebApi.DTOs;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAllReportsAsync();
        Task<ReportDto> GetReportByIdAsync(Guid id);
        Task<ReportDto> AddReportAsync(AddReportDto addReportDto);
        Task<ReportDto> UpdateReportAsync(Guid id, UpdateReportDto updateReportDto);
        Task<ReportDto> DeleteReportAsync(Guid id);
    }
}