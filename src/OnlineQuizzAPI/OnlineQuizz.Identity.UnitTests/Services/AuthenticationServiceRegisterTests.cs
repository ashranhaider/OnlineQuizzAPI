using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.Auth.Register.Commands;
using OnlineQuizz.Application.Models;
using OnlineQuizz.Application.Models.Authentication;
using OnlineQuizz.Identity.Contracts;
using OnlineQuizz.Identity.Models;
using OnlineQuizz.Identity.Services;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Identity.UnitTests.Services
{
    public class AuthenticationServiceRegisterTests
    {
        private static Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var options = Options.Create(new IdentityOptions());
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var userValidators = new List<IUserValidator<ApplicationUser>>();
            var passwordValidators = new List<IPasswordValidator<ApplicationUser>>();
            var normalizer = new UpperInvariantLookupNormalizer();
            var describer = new IdentityErrorDescriber();
            var services = new Mock<IServiceProvider>().Object;
            var logger = new Mock<ILogger<UserManager<ApplicationUser>>>().Object;

            return new Mock<UserManager<ApplicationUser>>(
                store.Object,
                options,
                passwordHasher,
                userValidators,
                passwordValidators,
                normalizer,
                describer,
                services,
                logger);
        }

        private static SignInManager<ApplicationUser> CreateSignInManager(UserManager<ApplicationUser> userManager)
        {
            return new SignInManager<ApplicationUser>(
                userManager,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                Options.Create(new IdentityOptions()),
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<ApplicationUser>>().Object);
        }

        private static Identity.Services.AuthenticationService CreateService(Mock<UserManager<ApplicationUser>> userManagerMock)
        {
            var jwtSettings = Options.Create(new JwtSettings
            {
                Key = "test-key-test-key-test-key-test-key",
                Issuer = "test",
                Audience = "test",
                DurationInMinutes = 60,
                RefreshTokenDurationInDays = 7
            });

            var googleSettings = Options.Create(new GoogleAuthSettings
            {
                ClientId = "test-client"
            });

            var refreshTokenRepo = new Mock<IRefreshTokenRepository>();

            return new Identity.Services.AuthenticationService(
                new Mock<ILogger<Identity.Services.AuthenticationService>>().Object,
                userManagerMock.Object,
                CreateSignInManager(userManagerMock.Object),
                jwtSettings,
                googleSettings,
                refreshTokenRepo.Object);
        }

        private static RegistrationRequest CreateValidRequest() => new RegistrationRequest
        {
            FirstName = "Alice",
            LastName = "Tester",
            Email = "alice@example.com",
            UserName = "alice",
            Password = "Password1!"
        };

        [Fact]
        public async Task RegisterAsync_WhenUsernameExists_ThrowsDomainException()
        {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock.Setup(x => x.FindByNameAsync("alice"))
                .ReturnsAsync(new ApplicationUser());

            var service = CreateService(userManagerMock);
            var request = CreateValidRequest();

            await Should.ThrowAsync<DomainException>(() => service.RegisterAsync(request));

            userManagerMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Never);
            userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task RegisterAsync_WhenEmailExists_ThrowsDomainException()
        {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock.Setup(x => x.FindByNameAsync("alice"))
                .ReturnsAsync((ApplicationUser?)null);
            userManagerMock.Setup(x => x.FindByEmailAsync("alice@example.com"))
                .ReturnsAsync(new ApplicationUser());

            var service = CreateService(userManagerMock);
            var request = CreateValidRequest();

            await Should.ThrowAsync<DomainException>(() => service.RegisterAsync(request));

            userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task RegisterAsync_WhenCreateFails_ThrowsValidationException()
        {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock.Setup(x => x.FindByNameAsync("alice"))
                .ReturnsAsync((ApplicationUser?)null);
            userManagerMock.Setup(x => x.FindByEmailAsync("alice@example.com"))
                .ReturnsAsync((ApplicationUser?)null);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Password1!"))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "bad" }));

            var service = CreateService(userManagerMock);
            var request = CreateValidRequest();

            var ex = await Should.ThrowAsync<ValidationException>(() => service.RegisterAsync(request));

            ex.Errors.ShouldContainKey("general");
            ex.Errors["general"].ShouldContain("bad");
        }

        [Fact]
        public async Task RegisterAsync_WhenSuccessful_ReturnsResponse()
        {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock.Setup(x => x.FindByNameAsync("alice"))
                .ReturnsAsync((ApplicationUser?)null);
            userManagerMock.Setup(x => x.FindByEmailAsync("alice@example.com"))
                .ReturnsAsync((ApplicationUser?)null);

            ApplicationUser? createdUser = null;

            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Password1!"))
                .Callback<ApplicationUser, string>((user, _) =>
                {
                    createdUser = user;
                    user.Id = "user-123";
                })
                .ReturnsAsync(IdentityResult.Success);

            var service = CreateService(userManagerMock);
            var request = CreateValidRequest();

            var response = await service.RegisterAsync(request);

            response.UserId.ShouldBe("user-123");
            response.Message.ShouldBe("User registered successfully");

            createdUser.ShouldNotBeNull();
            createdUser!.Email.ShouldBe(request.Email);
            createdUser.UserName.ShouldBe(request.UserName);
            createdUser.FirstName.ShouldBe(request.FirstName);
            createdUser.LastName.ShouldBe(request.LastName);
            createdUser.EmailConfirmed.ShouldBeTrue();

            userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Password1!"), Times.Once);
        }
    }
}
