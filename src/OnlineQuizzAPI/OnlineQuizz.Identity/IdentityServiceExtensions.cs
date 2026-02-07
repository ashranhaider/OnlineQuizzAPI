using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineQuizz.Application.Contracts.Identity;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Models;
using OnlineQuizz.Application.Models.Authentication;
using OnlineQuizz.Application.Responses;
using OnlineQuizz.Identity.Contracts;
using OnlineQuizz.Identity.Models;
using OnlineQuizz.Identity.Repositories;
using OnlineQuizz.Identity.Services;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace OnlineQuizz.Identity
{
    public static class IdentityServiceExtensions
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<GoogleAuthSettings>(configuration.GetSection("GoogleAuth"));

            services.AddDbContext<OnlineQuizzIdentityDbContext>(options =>
                 options.UseSqlServer(
                     configuration.GetConnectionString("OnlineQuizzConnectionString"),
                     sqlOptions =>
                     {
                         sqlOptions.EnableRetryOnFailure(
                             maxRetryCount: 5,
                             maxRetryDelay: TimeSpan.FromSeconds(10),
                             errorNumbersToAdd: null);
                     })
             );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<OnlineQuizzIdentityDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Disable Identity password rules because FluentValidation now handles them
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1; // min rule enforced by FluentValidation instead
            });

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<JwtBearerEventsHandler>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.SaveToken = false;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = configuration["JwtSettings:Issuer"],
                            ValidAudience = configuration["JwtSettings:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                            NameClaimType = ClaimTypes.NameIdentifier,   
                            RoleClaimType = ClaimTypes.Role
                        };

                        //options.EventsType = typeof(JwtBearerEventsHandler);
                    });
        }
    }
}
