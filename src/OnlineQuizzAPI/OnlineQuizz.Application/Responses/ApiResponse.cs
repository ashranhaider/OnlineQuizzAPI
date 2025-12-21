namespace OnlineQuizz.Application.Responses
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public object? Errors { get; set; }

        public static ApiResponse<T> Success(T data, string? message = null)
            => new() { Data = data, Message = message };

        public static ApiResponse<T> Failure(object errors, string? message = null)
            => new() { Errors = errors, Message = message };
    }
}
