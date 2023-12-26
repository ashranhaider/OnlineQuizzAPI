using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Domain.Common;
using OnlineQuizz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OnlineQuizz.Persistence
{
    public class OnlineQuizzDbContext: DbContext
    {
        private readonly ILoggedInUserService? _loggedInUserService;

        //public GloboTicketDbContext(DbContextOptions<GloboTicketDbContext> options)
        //   : base(options)
        //{
        //}

        public OnlineQuizzDbContext(DbContextOptions<OnlineQuizzDbContext> options, ILoggedInUserService loggedInUserService)
            : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<Quizz> Quizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineQuizzDbContext).Assembly);

            //seed data, added through migrations
            //var concertGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            //var musicalGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            //var playGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            //var conferenceGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            //modelBuilder.Entity<Category>().HasData(new Category
            //{
            //    CategoryId = concertGuid,
            //    Name = "Concerts"
            //});
            //modelBuilder.Entity<Category>().HasData(new Category
            //{
            //    CategoryId = musicalGuid,
            //    Name = "Musicals"
            //});
            //modelBuilder.Entity<Category>().HasData(new Category
            //{
            //    CategoryId = playGuid,
            //    Name = "Plays"
            //});
            //modelBuilder.Entity<Category>().HasData(new Category
            //{
            //    CategoryId = conferenceGuid,
            //    Name = "Conferences"
            //});

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
