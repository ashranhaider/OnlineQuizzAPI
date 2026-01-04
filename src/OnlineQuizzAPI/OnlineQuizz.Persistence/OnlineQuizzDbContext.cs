using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Domain.Common;
using OnlineQuizz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OnlineQuizz.Persistence
{
    public class OnlineQuizzDbContext: DbContext
    {
        private readonly ILoggedInUserService? _loggedInUserService;
        public DbSet<Quizz> Quizzes { get; set; } = null!;
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;

        public OnlineQuizzDbContext(DbContextOptions<OnlineQuizzDbContext> options, ILoggedInUserService loggedInUserService)
            : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineQuizzDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            ApplyAuditing();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditing();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditing()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy ??= _loggedInUserService?.UserId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = _loggedInUserService?.UserId;
                }
            }
        }
    }
}
