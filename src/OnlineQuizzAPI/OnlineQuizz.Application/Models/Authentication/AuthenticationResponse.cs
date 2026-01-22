
namespace OnlineQuizz.Application.Models.Authentication
{
    public class AuthenticationResponse
    {
        public required AuthenticatedUser User { get; set; }
        public required string AccessToken { get; set; }
        public DateTimeOffset AccessTokenExpiresIn { get; set; }
        public required string RefreshToken { get; set; }
    }
    public class AuthenticatedUser
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
