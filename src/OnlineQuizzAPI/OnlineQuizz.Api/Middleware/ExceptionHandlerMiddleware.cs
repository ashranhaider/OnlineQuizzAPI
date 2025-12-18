using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Models;
using System.Net;
using System.Text.Json;

namespace OnlineQuizz.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private async Task ConvertException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ErrorResponse response = new ErrorResponse
            {
                Code = "UNEXPECTED_ERROR",
                Message = "An unexpected error occurred"
            };

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Code = "VALIDATION_ERROR";
                    response.Message = "One or more validation errors occurred";
                    response.Errors = validationException.ValdationErrors;
                    break;

                case AuthenticationFailedException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Code = "AUTH_INVALID_CREDENTIALS";
                    response.Message = exception.Message;
                    break;

                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Code = "NOT_FOUND";
                    response.Message = "Resource not found";
                    break;

                case DomainException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    response.Code = "BUSINESS_RULE_VIOLATION";
                    response.Message = exception.Message;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            await context.Response.WriteAsJsonAsync(response);
        }

    }
}
