using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Middleware;

public class ExceprionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceprionHandlingMiddleware> _logger;


    public ExceprionHandlingMiddleware(RequestDelegate next, ILogger<ExceprionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            if (exception.InnerException != null)
            {
                await HandleExceptionAsync(httpContext, exception.InnerException, exception.Message);
            }
            else
            {
                await HandleExceptionAsync(httpContext, exception, "Произошла непредвиденная ошиюка");
            }
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex, string message)
    {
        _logger.LogError(ex.Message);
        _logger.LogError(ex.StackTrace);

        HttpResponse response = httpContext.Response;
        response.ContentType = "text/html; charset=utf-8";
        var url = httpContext.Request.GetDisplayUrl();

        await httpContext.Response.WriteAsync($"<h2>{message}</h2> <p><a href=\"{url}\" >Назад</a></p>");
    }
}