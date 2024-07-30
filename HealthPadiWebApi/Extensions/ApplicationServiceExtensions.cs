using FluentValidation;
using FluentValidation.AspNetCore;
using HealthPadiBackend.Validators;
using HealthPadiWebApi.MapperConfiguration;
using HealthPadiWebApi.Repositories.Implementations;
using HealthPadiWebApi.Repositories.Interfaces;
using HealthPadiWebApi.Services.Implementations;
using HealthPadiWebApi.Services.Interfaces;
namespace HealthPadiWebApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Registering Automapper
            services.AddSingleton<MappingProfiles>();
            services.AddAutoMapper(typeof(MappingProfiles));

            // Registering Fluent Validation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ReportValidation>();

            //Registering Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReportService, ReportService>();
           

            return services;
        }
    }
}
