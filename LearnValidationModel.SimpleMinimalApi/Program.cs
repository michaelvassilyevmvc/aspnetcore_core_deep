using System.ComponentModel.DataAnnotations;
using LearnValidationModel.SimpleMinimalApi;
using LearnValidationModel.SimpleMinimalAPI.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/employees", (Employee employee) =>
    {
        EmployeesRepository.AddEmployee(employee);
        return "Employee is added successfully";
    })
    .WithParameterValidation();

// from body
// app.MapPost("/auth", (Auth? auth) =>
// {
//     if (auth is not null)
//     {
//         return "Authorization is succeed!";
//     }
//     return  "Authorization is failed!";
// }).WithParameterValidation();

// from query
app.MapPost("/auth", (Auth? auth) =>
    {
        if (auth is not null) return "Authorization is succeed!";

        return "Authorization is failed!";
    })
    .WithParameterValidation();

app.Run();

internal class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

internal class Auth
{
    [Required]
    [EmailAddress]
    [FromQuery(Name = "email")]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    [FromQuery(Name = "pass")]
    public string Password { get; set; }

    [Required]
    [MatchesPassword]
    [FromQuery(Name = "conf")]
    public string ConfirmPassword { get; set; }

    public static ValueTask<Auth?> BindAsync(HttpContext context)
    {
        var emailStr = context.Request.Query["email"];
        var passStr = context.Request.Query["pass"];
        var confStr = context.Request.Query["conf"];

        if (!string.IsNullOrWhiteSpace(emailStr))
            return new ValueTask<Auth?>(new Auth
            {
                Email = emailStr,
                Password = passStr,
                ConfirmPassword = confStr
            });

        return new ValueTask<Auth?>(Task.FromResult<Auth?>(null));
    }
}