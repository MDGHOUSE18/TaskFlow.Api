using System.Net;
using System.Text.Json;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.Infrastructure.Data;

namespace TaskFlow.Api.Common
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;
        private readonly IServiceScopeFactory _scopeFactory;


        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment environment,IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _environment = environment;
            _scopeFactory= scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationException ex)
            {
                var message = _environment.IsDevelopment()
        ? ex.Message
        : "An unexpected error occurred.";
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    message);

            }
            catch (Exception ex)
            {
                await LogExceptionAsync(context, ex);

                var message = _environment.IsDevelopment()
                    ? ex.Message
                    : "An unexpected error occurred.";

                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    message);
            }

        }
        private static async Task HandleExceptionAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = ApiResponse<string>.Fail(message);

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        private async Task LogExceptionAsync(HttpContext context, Exception ex)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();
            var userId = context.User?.FindFirst("sub")?.Value;

            var error = new ApplicationError
            {
                Id = Guid.NewGuid(),
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source ?? "Unknown",
                Path = context.Request.Path,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            dbContext.ApplicationErrors.Add(error);
            await dbContext.SaveChangesAsync();
        }

    }
}
