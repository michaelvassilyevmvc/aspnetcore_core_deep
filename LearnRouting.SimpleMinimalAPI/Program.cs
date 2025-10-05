var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("pos", typeof(PostionContraint));
});

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/employees",
        async (HttpContext context) => { await context.Response.WriteAsync("Get Employees\r\n"); });
    endpoints.MapPost("/employees",
        async (HttpContext context) => { await context.Response.WriteAsync("Create Employee\r\n"); });
    endpoints.MapPut("/employees",
        async (HttpContext context) => { await context.Response.WriteAsync("Put Employees\r\n"); });
    endpoints.MapDelete("/employees/{id}",
        async (HttpContext context) =>
        {
            await context.Response.WriteAsync($"Delete Employee id is {context.Request.RouteValues["id"]}\r\n");
        });

    endpoints.MapGet("/{category=computer}/{id?}",
        async (HttpContext context) =>
        {
            await context.Response.WriteAsync(
                $"Get Employees. Category = {context.Request.RouteValues["category"]}. Id = {context.Request.RouteValues["id"]}\r\n");
        });

    endpoints.MapGet("/employees/position/{position:pos}",
        async (HttpContext context) =>
        {
            await context.Response.WriteAsync($"Get position is {context.Request.RouteValues["position"]}");
        });
});

app.Run();

class PostionContraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        if (!values.ContainsKey(routeKey)) return false;
        if(values[routeKey] is null) return false;
        
        if(values[routeKey].ToString().Equals("developer") || values[routeKey].ToString().Equals("manager")) return true;

        return false;
    }
}