using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Models;
using OnlineQuizz.Application.Responses;
using System.Net;

namespace OnlineQuizz.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ApiResponse<object> response;
            int statusCode;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    response = ApiResponse<object>.Failure(
                        message: "One or more validation errors occurred",
                        errors: validationException.ValdationErrors
                    );
                    break;

                case AuthenticationFailedException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    response = ApiResponse<object>.Failure(
                        exception.Message ?? "Invalid credentials"
                    );
                    break;

                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    response = ApiResponse<object>.Failure(
                        exception.Message ?? "Resource not found"
                    );
                    break;

                case DomainException:
                    statusCode = StatusCodes.Status409Conflict;
                    response = ApiResponse<object>.Failure(
                        exception.Message
                    );
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    if (_env.IsDevelopment())
                    {
                        response = ApiResponse<object>.Failure(
                        "Error: " + exception.Message + Environment.NewLine +
                                        (exception.InnerException != null ? exception.InnerException.Message : string.Empty)
                    );
                    }
                    else
                    {
                        response = ApiResponse<object>.Failure(
                        "An unexpected error occurred"
                        );
                    }

                    break;
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
