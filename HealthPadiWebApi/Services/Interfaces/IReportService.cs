﻿using HealthPadiWebApi.DTOs.Request;
using HealthPadiWebApi.DTOs.Response;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAllReportsAsync(string location);
        Task<ReportDto> GetReportByIdAsync(Guid id);
        Task<ReportDto> AddReportAsync(AddReportDto addReportDto, Guid userId);
        Task<ReportDto> DeleteReportAsync(Guid id);
    }
}