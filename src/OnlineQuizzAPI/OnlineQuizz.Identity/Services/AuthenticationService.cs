using OnlineQuizz.Application.Contracts.Identity;
using OnlineQuizz.Application.Models.Authentication;
using OnlineQuizz.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using OnlineQuizz.Application.Exceptions;

namespace OnlineQuizz.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            if (request == null)
            {
                throw new BadRequestException("Something went wrong. Please try again.");
            }

            if (request.Email == null || request.Email == "")
            {
                throw new BadRequestException("Email cannot be null");
            }

            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException($"User with {request.Email} not found.", request);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName ?? user.Email ?? "", request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
                throw new AuthenticationFailedException("Invalid email or password");

            string jwtSecurityToken = await GenerateJwtToken(user);

            AuthenticationResponse response = new AuthenticationResponse
            {
                User = new AuthenticatedUser
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email ?? "",
                    UserName = user.UserName ?? user.Email ?? ""
                },
                AccessToken = jwtSecurityToken,
                ExpiresIn = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };

            return response;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            if (request == null)
                throw new BadRequestException("Invalid request");

            if (await _userManager.FindByNameAsync(request.UserName) != null)
                throw new DomainException($"Username '{request.UserName}' already exists");

            if (await _userManager.FindByEmailAsync(request.Email) != null)
                throw new DomainException($"Email '{request.Email}' already exists");

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                List<FluentValidation.Results.ValidationFailure> errors = result.Errors
                    .Select(x => new FluentValidation.Results.ValidationFailure
                    {
                        ErrorCode = x.Code,
                        Severity = FluentValidation.Severity.Error,
                        ErrorMessage = x.Description
                    }).ToList();

                throw new ValidationException(new FluentValidation.Results.ValidationResult(errors));
            }

            return new RegistrationResponse
            {
                UserId = user.Id,
                Message = "User registered successfully"
            };
        }



        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            if (string.IsNullOrEmpty(user.SecurityStamp))
                throw new DomainException("User security stamp missing");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim("security_stamp", user.SecurityStamp ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(userClaims);

            claims.AddRange(roles.Select(role =>
                new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
