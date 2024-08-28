using Microsoft.EntityFrameworkCore;
using HealthPadiWebApi.Extensions;
using HealthPadiWebApi.Data;


namespace HealthPadiWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "https://dev--healthpadi.netlify.app/")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials();
                });
        });
        // Registers controllers and set up authorization policies.
        builder.Services.AddControllers();
        

        //Using the Extensions
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        builder.Services.AddAuthorization();

        //configuring Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // DI for Seed 
        builder.Services.AddTransient<Seed>();

        // Add DB context
        builder.Services.AddDbContext<HealthPadiDataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("HealthPadiDBConnection")));


        var app = builder.Build();

        if (args.Length == 1 && args[0] == "seed")
        {
            seedData(app).GetAwaiter().GetResult();
        }

        async Task seedData(IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<Seed>();
                if (service != null)
                {
                    await service.SeedDataContext();
                }
                else
                {
                    throw new InvalidOperationException("Unable to retrieve Seed service");
                }
            }
        }


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseCors();
        app.UseAuthorization();
        app.MapControllers();
        /*app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        app.UseMiddleware<GlobalJsonRequestFormatRequirementMiddleware>();*/
        app.Run();
    }

}