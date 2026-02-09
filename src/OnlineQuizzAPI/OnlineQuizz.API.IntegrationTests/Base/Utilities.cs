using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Persistence;
using System;

namespace OnlineQuizz.API.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(OnlineQuizzDbContext context)
        {
            const string testUserId = "test-user-id";
            var now = DateTime.UtcNow;

            context.Quizzes.Add(new Quizz
            {
                Id = 1,
                Name = "Concerts",
                OwnerUserId = testUserId,
                IsActive = true,
                CreatedDate = now
            });
            context.Quizzes.Add(new Quizz
            {
                Id = 2,
                Name = "Musicals",
                OwnerUserId = testUserId,
                IsActive = true,
                CreatedDate = now
            });
            context.Quizzes.Add(new Quizz
            {
                Id = 3,
                Name = "Plays",
                OwnerUserId = testUserId,
                IsActive = true,
                CreatedDate = now
            });
            context.Quizzes.Add(new Quizz
            {
                Id = 4,
                Name = "Conferences",
                OwnerUserId = testUserId,
                IsActive = true,
                CreatedDate = now
            });

            context.SaveChanges();
        }
    }
}
