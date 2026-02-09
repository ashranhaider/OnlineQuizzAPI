using OnlineQuizz.Persistence;
using OnlineQuizz.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace OnlineQuizz.API.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove app-registered DbContext configurations (SQL Server) before adding InMemory.
                services.RemoveAll<DbContextOptions<OnlineQuizzDbContext>>();
                services.RemoveAll<OnlineQuizzDbContext>();
                services.RemoveAll<IDbContextOptionsConfiguration<OnlineQuizzDbContext>>();

                services.RemoveAll<DbContextOptions<OnlineQuizzIdentityDbContext>>();
                services.RemoveAll<OnlineQuizzIdentityDbContext>();
                services.RemoveAll<IDbContextOptionsConfiguration<OnlineQuizzIdentityDbContext>>();

                services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
                        options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => { });

                services.AddDbContext<OnlineQuizzDbContext>(options =>
                {
                    options.UseInMemoryDatabase("OnlineQuizzDbContextInMemoryTest");
                });

                services.AddDbContext<OnlineQuizzIdentityDbContext>(options =>
                {
                    options.UseInMemoryDatabase("OnlineQuizzIdentityDbContextInMemoryTest");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<OnlineQuizzDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                };
            });
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }

        public HttpClient GetAuthenticatedClient()
        {
            return CreateClient();
        }
    }
}
