using OnlineQuizz.Application.Contracts.Identity;
using OnlineQuizz.Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizz.Application.Responses;

namespace OnlineQuizz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> RegisterAsync([FromBody] RegistrationRequest request)
        {
            RegistrationResponse response = await _authenticationService.RegisterAsync(request);

            ApiResponse<RegistrationResponse> apiResponse = new ApiResponse<RegistrationResponse>
            {
                Data = response,
                Message = "Registration successful"
            };
            return Ok(apiResponse);
        }
    }
}
