using OnlineQuizz.Application.Features.Auth.Register.Commands;
using OnlineQuizz.Application.Models.Authentication;

namespace OnlineQuizz.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<AuthenticationResponse> RefreshAsync(string refreshToken);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
