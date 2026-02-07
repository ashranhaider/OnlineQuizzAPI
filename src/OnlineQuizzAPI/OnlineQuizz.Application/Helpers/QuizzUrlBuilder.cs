namespace OnlineQuizz.Application.Helpers
{
    public static class QuizzUrlBuilder
    {
        private const string StartQuizzBasePath = "/startquizz/";

        public static string Build(int quizzId)
        {
            return $"{StartQuizzBasePath}{quizzId}";
        }
    }
}
