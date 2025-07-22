
using HealthCareManagementSystem.API.Controllers;
using HealthCareManagementSystem.Application.Services;
using HealthCareManagementSystem.Application.Services.MedicalRecordServices;
using HealthCareManagementSystem.Application.Services.PatientServices;
using HealthCareManagementSystem.Infrastructure;
using HealthCareManagementSystem.Infrastructure.Contracts;
using HealthCareManagementSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using HealthCareManagementSystem.Application.Services;

using HealthCareManagementSystem.Application.Services.AppointmentServices;
using HealthCareManagementSystem.Application.Services.MedicalRecordServices;
using HealthCareManagementSystem.Application.Services.DoctorServices;
using HealthCareManagementSystem.Application.Services.UserServices;


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

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        },
                        new List<string>()
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
            builder.Services.AddScoped<IAppointmentContract, AppointmentRepository>();

            builder.Services.AddScoped<IUserContract, UserRepository>();
            builder.Services.AddScoped<IDoctorContract, DoctorRepository>();
            
            // Service Dependencies
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddScoped<IUserService, UserService>();

            // JWT Authentication Configuration
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

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

            app.UseAuthentication();
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
