using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Persistence;
using System;

namespace OnlineQuizz.API.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(OnlineQuizzDbContext context)
        {            
            context.Quizzes.Add(new Quizz
            {
                Id = 1,
                Name = "Concerts"
            });
            context.Quizzes.Add(new Quizz
            {
                Id = 2,
                Name = "Musicals"
            });
            context.Quizzes.Add(new Quizz
            {
                Id = 3,
                Name = "Plays"
            });
            context.Quizzes.Add(new Quizz
            {
                Id = 4,
                Name = "Conferences"
            });

            context.SaveChanges();
        }
    }
}
