var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Get Employees\r\n");
    });
    endpoints.MapPost("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Create Employee\r\n");
    });
    endpoints.MapPut("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Put Employees\r\n");
    });
    endpoints.MapDelete("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Delete Employee\r\n");
    });
    
});

app.Run();