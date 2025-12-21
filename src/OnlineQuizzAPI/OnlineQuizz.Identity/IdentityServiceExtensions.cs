using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineQuizz.Application.Contracts.Identity;
using OnlineQuizz.Application.Models.Authentication;
using OnlineQuizz.Application.Responses;
using OnlineQuizz.Identity.Models;
using OnlineQuizz.Identity.Services;
using System.Text;
using System.Text.Json;

namespace OnlineQuizz.Identity
{
    public static class IdentityServiceExtensions
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

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

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = true; // 🔐 enable in prod
                o.SaveToken = false;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };

                o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext
                            .RequestServices
                            .GetRequiredService<UserManager<ApplicationUser>>();

                        var userId = context.Principal?
                            .FindFirst("uid")?.Value;

                        var stampClaim = context.Principal?
                            .FindFirst("security_stamp")?.Value;

                        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(stampClaim))
                        {
                            context.Fail("Invalid token claims");
                            return;
                        }

                        var user = await userManager.FindByIdAsync(userId);

                        if (user == null || user.SecurityStamp != stampClaim)
                        {
                            context.Fail("Token has been revoked");
                        }
                    },

                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var response = ApiResponse<object>.Failure(
                            "Authentication failed");

                        return context.Response.WriteAsJsonAsync(response);
                    },

                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var response = ApiResponse<object>.Failure(
                            "Unauthorized");

                        return context.Response.WriteAsJsonAsync(response);
                    },

                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var response = ApiResponse<object>.Failure(
                            "Forbidden");

                        return context.Response.WriteAsJsonAsync(response);
                    }
                };
            });
        }
    }
}
