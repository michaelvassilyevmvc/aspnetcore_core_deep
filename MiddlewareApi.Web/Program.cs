using MiddlewareApi.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CustomMiddleware>();
builder.Services.AddTransient<CustomExceptionHandingMiddleware>();

var app = builder.Build();
app.UseMiddleware<CustomExceptionHandingMiddleware>();
// Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Before Middleware #1\r\n");
    throw new Exception("Middleware #2 Exception");
    await next(context);
    await context.Response.WriteAsync("After Middleware #1\r\n");
});

;
app.UseMiddleware<CustomMiddleware>();

// Middleware #2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Before Middleware #2\r\n");
    await next(context);
    await context.Response.WriteAsync("After Middleware #2\r\n");
});

// Middleware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Before Middleware #3\r\n");
    await next(context);
    await context.Response.WriteAsync("After Middleware #3\r\n");
});

app.Run();