using AutoMapper;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.MapperConfiguration
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<UpdateReportDto, Report>().ReverseMap();
            CreateMap<Report, AddReportDto>().ReverseMap();
        }
    }
}
