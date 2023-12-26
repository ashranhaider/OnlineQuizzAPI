using OnlineQuizz.Application.Contracts.Infrastructure;
using OnlineQuizz.Application.Models.Mail;
using OnlineQuizz.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineQuizz.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
