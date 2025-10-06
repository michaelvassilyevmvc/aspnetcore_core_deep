using System.Globalization;
using System.Text.Json;
using LearnRouting.SimpleMinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/employees", async context =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("<h1>Employees</h1>");
        foreach (Employee employee in EmployeesRepository.GetEmployees())
        {
            await context.Response.WriteAsync($"<p><b>Id: </b>{employee.Id}</p>");
            await context.Response.WriteAsync($"<p><b>Name: </b>{employee.Name}</p>");
            await context.Response.WriteAsync($"<p><b>Position: </b>{employee.Position}</p>");
            await context.Response.WriteAsync(
                $"<p><b>Salary: </b>{employee.Salary.ToString("C", new CultureInfo("us-US"))}</p>");
            await context.Response.WriteAsync($"<hr />");
        }
    });

    endpoints.MapGet("/employees/{id:int}", async context =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("<h1>Employees</h1>");
        if (int.TryParse(context.Request.RouteValues["id"]
                .ToString(), out var id))
        {
            var employee = EmployeesRepository.GetEmployeeById(id);
            await context.Response.WriteAsync($"<p><b>Id: </b>{employee.Id}</p>");
            await context.Response.WriteAsync($"<p><b>Name: </b>{employee.Name}</p>");
            await context.Response.WriteAsync($"<p><b>Position: </b>{employee.Position}</p>");
            await context.Response.WriteAsync(
                $"<p><b>Salary: </b>{employee.Salary.ToString("C", new CultureInfo("us-US"))}</p>");
            await context.Response.WriteAsync($"<hr />");
        }
    });

    endpoints.MapPost("/employees", async context =>
    {
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        context.Response.ContentType = "text/html";
        var employee =  JsonSerializer.Deserialize<Employee>(body);
        if (employee is not null)
        {
            context.Response.StatusCode = 201;
            EmployeesRepository.AddEmployee(employee);
            await context.Response.WriteAsync("Employee is inserted!");
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Employee does not exist!");
        }
    });
    
    endpoints.MapPut("/employees/{id}", async context =>
    {
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        var employee =  JsonSerializer.Deserialize<Employee>(body);

        if (int.TryParse(context.Request.RouteValues["id"].ToString(), out var id))
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
});

app.Run();