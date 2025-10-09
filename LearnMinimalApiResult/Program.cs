using LearnMinimalApiResult.Endpoints;
using LearnMinimalApiResult.Models;
using LearnMinimalApiResult.Results;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();


app.MapGet("/", HtmlResult () =>
{
    string html = "<h2>Welcome to our API</h2> Our API is used to learn ASP.NET";
    return new HtmlResult(html);
});

app.MapEmployeeEndpoints();

app.Run();