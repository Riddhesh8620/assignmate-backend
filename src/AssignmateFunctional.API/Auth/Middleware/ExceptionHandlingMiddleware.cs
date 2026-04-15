namespace AssignmateFunctional.API.Auth.Middleware;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
	private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";

		// You can customize the status code based on exception type
		context.Response.StatusCode = exception switch
		{
			UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
			KeyNotFoundException => StatusCodes.Status404NotFound,
			_ => StatusCodes.Status400BadRequest // Default to your current logic
		};

		return context.Response.WriteAsJsonAsync(new
		{
			error = exception.Message
		});
	}
}