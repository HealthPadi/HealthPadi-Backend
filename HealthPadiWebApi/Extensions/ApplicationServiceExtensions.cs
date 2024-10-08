﻿using FluentValidation;
using FluentValidation.AspNetCore;
using HealthPadiBackend.Validators;
using HealthPadiWebApi.MapperConfiguration;
using HealthPadiWebApi.Repositories.Implementations;
using HealthPadiWebApi.Repositories.Interfaces;
using HealthPadiWebApi.Services.Implementations;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
namespace HealthPadiWebApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient();
            // Registering Automapper
            services.AddSingleton<MappingProfiles>();
            services.AddAutoMapper(typeof(MappingProfiles));

            // Registering Fluent Validation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ReportValidation>();

            //Registering Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IHealthyLivingTopicsService, HealthyLivingTopicsService>();
            services.AddScoped<ITaskExecutionLoggerService, TaskExecutionLoggerService>();
            services.AddScoped<IAIService, AIService>();
            services.AddHostedService<WeeklyTaskService>();
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<IUserService, UserService>();

            // Adding Authentication and Authorozation to Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthPadi API", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                             Type = ReferenceType.SecurityScheme,
                             Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = "Oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header
                    },
                     new List<string>()
                }
                });
            });
            return services;
        }
    }
}
