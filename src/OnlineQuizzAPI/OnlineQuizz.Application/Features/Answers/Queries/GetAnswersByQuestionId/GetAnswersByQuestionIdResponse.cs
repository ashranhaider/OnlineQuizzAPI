namespace OnlineQuizz.Application.Features.Answers.Queries.GetAnswersByQuestionId
{
    public class GetAnswersByQuestionIdResponse
    {
        public int Id { get; set; }
        public string? AnswerText { get; set; }
        public string? QuestionText { get; set; }
        public string? SelectedQuestionOption { get; set; }
        public int QuestionId { get; set; }
    }
} 