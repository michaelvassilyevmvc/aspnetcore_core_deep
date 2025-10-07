using System.ComponentModel.DataAnnotations;
using LearnValidationModel.SimpleMinimalApi;
using LearnValidationModel.SimpleMinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/employees", (Employee employee) =>
{
    EmployeesRepository.AddEmployee(employee);
    return "Employee is added successfully";
}).WithParameterValidation();

app.MapPost("/auth", (Auth? auth) =>
{
    if (auth is not null)
    {
        return "Authorization is succeed!";
    }
    return  "Authorization is failed!";
}).WithParameterValidation();

app.Run();

class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

class Auth
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    [Required]
    [MatchesPassword]
    public string ConfirmPassword { get; set; }
    
    
}