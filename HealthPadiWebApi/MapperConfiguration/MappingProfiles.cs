﻿using AutoMapper;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.MapperConfiguration
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Feed, FeedDto>().ReverseMap();
            CreateMap<AddFeedDto, Feed>().ReverseMap();
            CreateMap<UpdateReportDto, Report>().ReverseMap();
            CreateMap<Report, AddReportDto>().ReverseMap();
            CreateMap<TaskExecutionLogDto, TaskExecutionLog>().ReverseMap();
            CreateMap<RegisterRequestDto, User>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email ));
        }
    }
    
}
