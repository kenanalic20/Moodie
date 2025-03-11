using Moodie.Data;
using System.Net;

namespace Moodie.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, ApplicationDbContext context)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex, context);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, ApplicationDbContext dbContext)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Log the error to the database
        await LogErrorToDatabase(exception, dbContext);

        // Return error response to the client
        var error = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = "An error has occurred. Please try again later."
        }.ToString();

        _logger.LogError($"Error: {error}");
        await context.Response.WriteAsJsonAsync(error);
    }

    private async Task LogErrorToDatabase(Exception exception, ApplicationDbContext dbContext)
    {
        var errorDetails = new ErrorDetails
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = exception.Message,
            StackTrace = exception.StackTrace
        };

        dbContext.ErrorDetails.Add(errorDetails);
        await dbContext.SaveChangesAsync();
    }
}
public class ErrorDetails
{
    public int Id { get; set; }

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}