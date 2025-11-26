using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ByteBuy.API.Middleware;

/// <summary>
/// Middleware that catches any uncaught exceptions and sends to user ProblemDetails response
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            if(ex is TaskCanceledException or OperationCanceledException)
            {
                _logger.LogInformation("Request canceled by user");
            }

            if(ex.InnerException is not null)
            {
                _logger.LogError($"{ex.InnerException.GetType().ToString()}:" +
                    $" {ex.InnerException.Message}");
            }

            string message = $"{ex.GetType()}: {ex.Message}";
            _logger.LogError(message);

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problemDetails = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server error has occured",
            };

            string json = JsonSerializer.Serialize(problemDetails);

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(json);
        }
        
    }
}
public static class GlobalExceptionHandlingMiddlewareExtensions
{
    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static IApplicationBuilder UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
