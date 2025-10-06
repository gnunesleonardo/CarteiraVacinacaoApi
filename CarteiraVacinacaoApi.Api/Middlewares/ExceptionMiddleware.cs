using FluentValidation;
using System.Net;
using System.Text.Json;

namespace CarteiraVacinacaoApi.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            object response;
            HttpStatusCode statusCode;

            if (exception is ValidationException validationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    StatusCode = (int)statusCode,
                    Message = "Erro de validação.",
                    Errors = validationException.Errors.Select(e => new
                    {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage
                    }),
                };
            }
            else if (exception is KeyNotFoundException notFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                response = new
                {
                    StatusCode = (int)statusCode,
                    notFoundException.Message,
                };
            }
            else if (exception is ArgumentException argumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    StatusCode = (int)statusCode,
                    argumentException.Message,
                };
            }
            else if (exception is UnauthorizedAccessException unauthorizedException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                response = new
                {
                    StatusCode = (int)statusCode,
                    unauthorizedException.Message,
                };
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                response = new
                {
                    StatusCode = (int)statusCode,
                    Message = "Erro interno no servidor.",
                };
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
