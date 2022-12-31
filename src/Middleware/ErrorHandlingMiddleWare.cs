using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Services.DiscordLogger;

namespace ScriptShoesCQRS.Middleware;

public class ErrorHandlingMiddleWare : IMiddleware 
{
    private readonly IDiscordLoggerService _logger;

    public ErrorHandlingMiddleWare(IDiscordLoggerService logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = 404;
            await _logger.LogException("Not found", ex.Message, DiscordLoggerColors.DarkAqua);
            await context.Response.WriteAsync(ex.Message);
        }
        catch (ConflictException ex)
        {
            context.Response.StatusCode = 409;
            await _logger.LogException("Conflict detected", ex.Message, DiscordLoggerColors.Red);
            await context.Response.WriteAsync(ex.Message);
        }
        catch (PermissionDeniedException ex)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await _logger.LogServerError("SERVER ERROR!!!", ex.Message, DiscordLoggerColors.DarkPurple);
            await context.Response.WriteAsync($"Something went wrong.");
        }
    }
}