using System.Text.Json;
using LearnRouting.SimpleMinimalAPI.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // endpoints.MapGet("/employees", async context =>
    // {
    //     context.Response.ContentType = "text/html";
    //     await context.Response.WriteAsync("<h1>Employees</h1>");
    //     foreach (Employee employee in EmployeesRepository.GetEmployees())
    //     {
    //         await context.Response.WriteAsync($"<p><b>Id: </b>{employee.Id}</p>");
    //         await context.Response.WriteAsync($"<p><b>Name: </b>{employee.Name}</p>");
    //         await context.Response.WriteAsync($"<p><b>Position: </b>{employee.Position}</p>");
    //         await context.Response.WriteAsync(
    //             $"<p><b>Salary: </b>{employee.Salary.ToString("C", new CultureInfo("us-US"))}</p>");
    //         await context.Response.WriteAsync($"<hr />");
    //     }
    // });

    // endpoints.MapGet("/employees/{id:int}", async context =>
    // {
    //     context.Response.ContentType = "text/html";
    //     await context.Response.WriteAsync("<h1>Employees</h1>");
    //     if (int.TryParse(context.Request.RouteValues["id"]
    //             .ToString(), out var id))
    //     {
    //         var employee = EmployeesRepository.GetEmployeeById(id);
    //         await context.Response.WriteAsync($"<p><b>Id: </b>{employee.Id}</p>");
    //         await context.Response.WriteAsync($"<p><b>Name: </b>{employee.Name}</p>");
    //         await context.Response.WriteAsync($"<p><b>Position: </b>{employee.Position}</p>");
    //         await context.Response.WriteAsync(
    //             $"<p><b>Salary: </b>{employee.Salary.ToString("C", new CultureInfo("us-US"))}</p>");
    //         await context.Response.WriteAsync($"<hr />");
    //     }
    // });

    endpoints.MapPost("/employees", (Employee employee) =>
    {
        if (employee is null || employee.Id <= 0) return "Employee is not provided or is not valid.";

        EmployeesRepository.AddEmployee(employee);
        return "Employee added successfully.";
    });

    endpoints.MapPut("/employees/{id}", async context =>
    {
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        var employee = JsonSerializer.Deserialize<Employee>(body);

        if (int.TryParse(context.Request.RouteValues["id"]
                .ToString(), out var id))
        {
            employee.Id = id;
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Employee does not exist!");
        }

        if (employee is not null)
        {
            context.Response.StatusCode = 201;
            EmployeesRepository.UpdateEmployee(employee);
            await context.Response.WriteAsync("Employee is updated!");
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Employee does not exist!");
        }
    });

    endpoints.MapDelete("/employees/{id}", async context =>
    {
        var employee = EmployeesRepository.GetEmployeeById(int.Parse(context.Request.RouteValues["id"]
            .ToString()));
        if (employee is not null)
        {
            context.Response.StatusCode = 201;
            EmployeesRepository.DeleteEmployee(employee.Id);
            await context.Response.WriteAsync("Employee is deleted!");
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Employee does not exist!");
        }
    });

    endpoints.MapGet("/employees", ([FromQuery(Name = "id")] int[] ids) =>
    {
        var employees = EmployeesRepository.GetEmployees();
        var res = employees.Where(x => ids.Contains(x.Id))
            .ToList();
        return res;
    });

    endpoints.MapGet("/people", (Person? p) => { return $"Id is {p?.Id}; Name is {p?.Name}"; });
});

app.Run();

internal class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public static ValueTask<Person?> BindAsync(HttpContext context)
    {
        var idStr = context.Request.Query["id"];
        var nameStr = context.Request.Query["name"];

        if (int.TryParse(idStr, out var id))
            return new ValueTask<Person?>(new Person
            {
                Id = id,
                Name = nameStr
            });

        return new ValueTask<Person?>(Task.FromResult<Person?>(null));
    }
}

internal class GetEmployeeParameter
{
    [FromRoute] public int Id { get; set; }
    [FromQuery] public string Name { get; set; }
    [FromHeader] public string Position { get; set; }
}