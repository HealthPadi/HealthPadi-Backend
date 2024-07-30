using AutoMapper;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Repositories.Interfaces;
using HealthPadiWebApi.Services.Interfaces;

namespace HealthPadiWebApi.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<ReportDto>> GetAllReportsAsync()
        {
            var reports = await unitOfWork.Report.GetAll();
            return mapper.Map<List<ReportDto>>(reports);
        }

        public async Task<ReportDto> GetReportByIdAsync(Guid id)
        {
            var report = await unitOfWork.Report.GetByIdAsync(id);
            return mapper.Map<ReportDto>(report);
        }

        public async Task<ReportDto> AddReportAsync(AddReportDto addReportDto)
        {
            var report = mapper.Map<Report>(addReportDto);
            await unitOfWork.Report.AddAsync(report);
            await unitOfWork.CompleteAsync();
            return mapper.Map<ReportDto>(report);
        }

        public async Task<ReportDto> UpdateReportAsync(Guid id, UpdateReportDto updateReportDto)
        {
            var existingReport = mapper.Map<Report>(updateReportDto);
            await unitOfWork.Report.UpdateReport(id, existingReport);
            if (existingReport == null)
            {
                return null;
            }
            await unitOfWork.CompleteAsync();
            return mapper.Map<ReportDto>(existingReport);
        }

        public async Task<ReportDto> DeleteReportAsync(Guid id)
        {
            var existingReport = await unitOfWork.Report.DeleteAsync(id);
            await unitOfWork.CompleteAsync();
            return mapper.Map<ReportDto>(existingReport);
        }
    }
}