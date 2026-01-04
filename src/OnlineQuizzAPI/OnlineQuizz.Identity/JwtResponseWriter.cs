using Microsoft.AspNetCore.Http;
using OnlineQuizz.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Identity
{
    public static class JwtResponseWriter
    {
        public static Task Write401(HttpContext context, string message)
        {
            if (context.Response.HasStarted)
                return Task.CompletedTask;

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsJsonAsync(ApiResponse<object>.Failure(message));
        }

        public static Task Write403(HttpContext context, string message)
        {
            if (context.Response.HasStarted)
                return Task.CompletedTask;

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsJsonAsync(ApiResponse<object>.Failure(message));
        }
    }
}
