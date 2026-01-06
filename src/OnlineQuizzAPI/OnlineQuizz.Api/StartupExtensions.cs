using OnlineQuizz.Api.Middleware;
using OnlineQuizz.Api.Services;
using OnlineQuizz.Api.Utility;
using OnlineQuizz.Application;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Identity;
using OnlineQuizz.Infrastructure;
using OnlineQuizz.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
//using Serilog;

namespace OnlineQuizz.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
        {
            AddSwagger(builder.Services);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendApp", policy =>
                {
                    policy
                        .WithOrigins(corsOrigins ?? [""])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    // .AllowCredentials(); // only if using cookies
                });
            });

            return builder.Build();

        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseCustomExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineQuizz API");
                });
            }

            app.UseHttpsRedirection();

            app.UseCors("FrontendApp");

            // 3. AUTHENTICATION & AUTHORIZATION
            app.UseAuthentication();
            app.UseAuthorization();

            // 4. ENDPOINTS
            app.MapGet("/", () => Results.Ok("OnlineQuizz API is running"));
            app.MapControllers();

            return app;
        }
        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OnlineQuizz API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http, // 🔥 IMPORTANT
                    Scheme = "bearer",               // 🔥 lowercase
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token only. Example: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

    }
}