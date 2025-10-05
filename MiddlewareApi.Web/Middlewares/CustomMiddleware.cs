namespace MiddlewareApi.Web.Middlewares;

public class CustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("Before Custom Middleware\r\n");
        await next(context);
        await context.Response.WriteAsync("After Custom Middleware\r\n");
    }
}