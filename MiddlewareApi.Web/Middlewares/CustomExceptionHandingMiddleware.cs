namespace MiddlewareApi.Web.Middlewares;

public class CustomExceptionHandingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            context.Response.ContentType = "text/html";
            await next(context);
        }
        catch (Exception ex)
        {
            // context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"<p>ErrorMessage: {ex.Message}</p>");
        }
    }
}