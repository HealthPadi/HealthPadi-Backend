using AutoMapper;
using HealthPadiWebApi.DTOs.Request;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Repositories.Interfaces;
using HealthPadiWebApi.Services.Interfaces;

namespace HealthPadiWebApi.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ReportDto>> GetAllReportsAsync(String location)
        {
            if (string.IsNullOrWhiteSpace(location) == false)
            {
                var reportLocation = await _unitOfWork.Report.GetReportsByLocation(location);
                return _mapper.Map<List<ReportDto>>(reportLocation);
            }
            var reports = await _unitOfWork.Report.GetAll();
            return _mapper.Map<List<ReportDto>>(reports);
        }
        public async Task<ReportDto> GetReportByIdAsync(Guid id)
        {
            var report = await _unitOfWork.Report.GetByIdAsync(id);
            return _mapper.Map<ReportDto>(report);
        }

        public async Task<ReportDto> AddReportAsync(AddReportDto addReportDto)
        {
            var report = _mapper.Map<Report>(addReportDto);
            await _unitOfWork.Report.AddAsync(report);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReportDto>(report);
        }

        public async Task<ReportDto> DeleteReportAsync(Guid id)
        {
            var existingReport = await _unitOfWork.Report.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReportDto>(existingReport);
        }
    }
}