using LibrarySystem.Errors;
using System.Net;
using System.Text.Json;

namespace LibrarySystem.Middlewares;

public class HandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HandlerMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public HandlerMiddleware(RequestDelegate next, ILogger<HandlerMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = _env.IsDevelopment() ?
                new ApiExceptionResponse(500, ex.Message, ex.StackTrace) :
                new ApiExceptionResponse(500);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var errorText = JsonSerializer.Serialize(errorResponse, options);

            await context.Response.WriteAsync(errorText);
        }
    }
}
