using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineQuizz.Identity.Models;
using System.Security.Claims;

namespace OnlineQuizz.Identity
{
    public class JwtBearerEventsHandler : JwtBearerEvents
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtBearerEventsHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //public override async Task TokenValidated(TokenValidatedContext context)
        //{
        //    var userId = context.Principal?
        //        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    var stampClaim = context.Principal?
        //        .FindFirst("security_stamp")?.Value;

        //    if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(stampClaim))
        //    {
        //        context.Fail("Missing user id or security stamp");
        //        return;
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null || user.SecurityStamp != stampClaim)
        //    {
        //        context.Fail("Token revoked");
        //    }
        //}

        //public override Task AuthenticationFailed(AuthenticationFailedContext context)
        //{
        //    context.NoResult();

        //    if (context.Exception is SecurityTokenExpiredException)
        //        return JwtResponseWriter.Write401(context.HttpContext, "Token expired");

        //    return JwtResponseWriter.Write401(context.HttpContext, "Authentication failed");
        //}

        //public override Task Challenge(JwtBearerChallengeContext context)
        //{
        //    context.HandleResponse();

        //    if (context.Response.HasStarted)
        //        return Task.CompletedTask;

        //    return JwtResponseWriter.Write401(context.HttpContext, "Unauthorized");
        //}

        //public override Task Forbidden(ForbiddenContext context)
        //{
        //    return JwtResponseWriter.Write403(context.HttpContext, "Forbidden");
        //}
    }
}
