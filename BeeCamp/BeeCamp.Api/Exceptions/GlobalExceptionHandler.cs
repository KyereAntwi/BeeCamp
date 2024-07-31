namespace BeeCamp.Api.Exceptions;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occured: {Message}", exception.Message);

        var problemDetails = new BaseResponse<string>("Exception")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Errors = new [] {"Server error"}
        };

        httpContext.Response.StatusCode = problemDetails.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}