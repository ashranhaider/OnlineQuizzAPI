using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineQuizz.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OnlineQuizzDbContext>(options =>
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

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IQuizzRepository, QuizzRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();

            return services;    
        }
    }
}
