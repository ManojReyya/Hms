
using HealthCareManagementSystem.API.Controllers;
using HealthCareManagementSystem.Application.Services;
using HealthCareManagementSystem.Application.Services.MedicalRecordServices;
using HealthCareManagementSystem.Application.Services.PatientServices;
using HealthCareManagementSystem.Infrastructure;
using HealthCareManagementSystem.Infrastructure.Contracts;
using HealthCareManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container

            

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = false;
                });

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Healthcare Management System API",
                    Version = "v1",
                    Description = "API for managing healthcare operations including medical records, patients, doctors, and appointments",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Healthcare Management Team"
                    }
                });
                
                // Enable XML comments for better API documentation
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            // Database Context - using the existing OnConfiguring method
            builder.Services.AddDbContext<HealthCareManagementSystemDbContext>();
            

            // Repository Dependencies
            builder.Services.AddScoped<IMedicalRecordContract, MedicalRecordRepository>();
            builder.Services.AddScoped<IPatientContract, PatientRepository>();

            // Service Dependencies
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IPatientService, PatientService>();

            // CORS Configuration for frontend integration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("HealthcarePolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Add logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Healthcare Management System API v1");
                    c.RoutePrefix = "swagger"; // Set Swagger UI at the app's root
                    c.DocumentTitle = "Healthcare Management System API";
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Enable CORS
            app.UseCors("HealthcarePolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            // Health check endpoint
            app.MapGet("/health", () => Results.Ok(new { 
                Status = "Healthy", 
                Timestamp = DateTime.UtcNow,
                Service = "Healthcare Management System API"
            }));

            app.Run();
        }
    }
}
