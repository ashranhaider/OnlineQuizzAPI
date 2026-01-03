using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineQuizz.Application.Contracts.Identity;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.Auth.Register.Commands;
using OnlineQuizz.Application.Models.Authentication;
using OnlineQuizz.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineQuizz.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        #region Authentication / Login
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                       ?? throw new AuthenticationFailedException("Invalid email or password");

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName ?? user.Email!,
                request.Password,
                false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                throw new AuthenticationFailedException("Invalid email or password");

            return new AuthenticationResponse
            {
                User = new AuthenticatedUser
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email ?? "",
                    UserName = user.UserName ?? user.Email ?? ""
                },
                AccessToken = await GenerateJwtToken(user),
                ExpiresIn = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };
        }
        #endregion


        #region Registration
        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
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

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    new FluentValidation.Results.ValidationResult(
                        result.Errors.Select(e => new FluentValidation.Results.ValidationFailure(
                            "general", e.Description)).ToList()
                    )
                );
            }

            return new RegistrationResponse
            {
                UserId = user.Id,
                Message = "User registered successfully"
            };
        }
        #endregion


        #region JWT Token
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            if (string.IsNullOrEmpty(user.SecurityStamp))
                throw new DomainException("User security stamp missing");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim("security_stamp", user.SecurityStamp ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(await _userManager.GetClaimsAsync(user));

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
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
        #endregion
    }
}
