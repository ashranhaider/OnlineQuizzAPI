using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizz.Application.Contracts.Identity;
using OnlineQuizz.Application.Features.Auth.Register.Commands;
using OnlineQuizz.Application.Models.Authentication;
using OnlineQuizz.Application.Requests;
using OnlineQuizz.Application.Responses;

namespace OnlineQuizz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;
        public AccountController(IAuthenticationService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }
        /*TODO: Password change         
            ➡ await _userManager.UpdateSecurityStampAsync(user);
            ➡ All JWTs invalid
            ➡ All refresh tokens revoked (you should revoke manually)

        Update stamp on:

            password change

            role change

            logout all

            refresh token abuse
         */
        [HttpPost("authenticate")]
        public async Task<ActionResult<ApiResponse<AuthenticationResponse>>> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            AuthenticationResponse response = await _authenticationService.AuthenticateAsync(request);
            
            ApiResponse<AuthenticationResponse> apiResponse = new ApiResponse<AuthenticationResponse>
            {
                Data = response,
                Message = "Authentication successful"
            };

            return Ok(apiResponse);
        }
        [HttpPost("google")]
        public async Task<ActionResult<ApiResponse<AuthenticationResponse>>> GoogleLogin(GoogleAuthRequest request)
        {
            var response = await _authenticationService.GoogleLoginAsync(request.IdToken);

            ApiResponse<AuthenticationResponse> apiResponse = new ApiResponse<AuthenticationResponse>
            {
                Data = response,
                Message = "Authentication successful"
            };

            return Ok(apiResponse);
        }
        [HttpPost("refreshtoken")]
        public async Task<ActionResult<ApiResponse<AuthenticationResponse>>> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            AuthenticationResponse response = await _authenticationService.RefreshAsync(request.RefreshToken);

            ApiResponse<AuthenticationResponse> apiResponse = new ApiResponse<AuthenticationResponse>
            {
                Data = response,
                Message = "Authentication successful"
            };

            return Ok(apiResponse);
        }


        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> RegisterAsync([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<RegistrationResponse>.Success(result, "User Registered"));
        }
    }
}
