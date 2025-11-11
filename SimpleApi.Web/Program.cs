using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async context =>
{
    context.Response.Headers["Content-Type"] = "text/html";
    if (context.Request.Path.Equals("/"))
    {
        await context.Response.WriteAsync("<h1>HEADERS</h1>");
        foreach (var header in context.Request.Headers)
            await context.Response.WriteAsync($"<p><b>{header.Key}</b>: {header.Value}</p>");
    }
    else if (context.Request.Path.StartsWithSegments("/employees"))
    {
        if (context.Request.Method == "GET")
        {
            if (context.Request.Query.TryGetValue("id", out var id))
            {
                var employee = EmployeesRepository.GetEmployee(int.Parse(id));
                if (employee is not null)
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync($"<h1>Employee ID: {employee.id}</h1>");
                    await context.Response.WriteAsync($"<p>Name:<i> {employee.name}</i></p>");
                }
                else
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync($"<h1>Employee ID = {id}  not found</h1>");
                }
            }
            else
            {
                await context.Response.WriteAsync("<h1>List of Employees!</h1>");
                foreach (var employee in EmployeesRepository.Employees)
                    await context.Response.WriteAsync($"<p><b>{employee.id}</b>{employee.name}</p>");
            }
        }
        else if (context.Request.Method == "POST")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var employee = JsonSerializer.Deserialize<Employee>(body);
            if (employee is not null)
            {
                EmployeesRepository.AddEmployee(employee);
                context.Response.StatusCode = 205;
            }
        }
        else if (context.Request.Method == "PUT")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var employee = JsonSerializer.Deserialize<Employee>(body);
            if (employee is not null)
            {
                var id = int.Parse(context.Request.Query["id"]);
                if (EmployeesRepository.UpdateEmployee(id, employee))
                    context.Response.StatusCode = 200;
                else
                    context.Response.StatusCode = 404;
            }
        }
        else if (context.Request.Method == "DELETE")
        {
            if (context.Request.Query.ContainsKey("id"))
            {
                int.TryParse(context.Request.Query["id"], out var id);
                if (EmployeesRepository.DeleteEmployee(id))
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("Employee with id " + id + " was deleted.");
                }
                else
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("Employee with id " + id + " was not found.");
                }
            }
        }
    }
});

app.Run();

public record Employee(int id, string name);

public static class EmployeesRepository
{
    static EmployeesRepository()
    {
        Employees = new List<Employee>
        {
            new(1, "Alice Johnson"),
            new(2, "Bob Smith"),
            new(3, "Charlie Brown"),
            new(4, "Diana Prince"),
            new(5, "Ethan Clark"),
            new(6, "Fiona Davis"),
            new(7, "George Miller"),
            new(8, "Hannah Wilson"),
            new(9, "Ian Thomas"),
            new(10, "Julia Roberts")
        };
    }

    public static List<Employee> Employees { get; set; }

    public static Employee? GetEmployee(int id)
    {
        return Employees.FirstOrDefault(x => x.id == id);
    }

    public static void AddEmployee(Employee employee)
    {
        var max = Employees.Max(x => x.id);
        employee = new Employee(max + 1, employee.name);
        Employees.Add(employee);
    }

    public static bool UpdateEmployee(int id, Employee updateEmployee)
    {
        var employee = Employees.FirstOrDefault(e => e.id == id);
        if (employee is not null)
        {
            employee = updateEmployee;
            return true;
        }

        return false;
    }

    public static bool DeleteEmployee(int id)
    {
        var employee = Employees.FirstOrDefault(e => e.id == id);
        if (employee is not null)
        {
            Employees.Remove(employee);
            return true;
        }

        return false;
    }
}