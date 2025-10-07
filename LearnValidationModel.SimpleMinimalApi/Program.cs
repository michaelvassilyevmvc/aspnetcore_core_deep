using LearnValidationModel.SimpleMinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/employees", (Employee employee) =>
{
    EmployeesRepository.AddEmployee(employee);
    return "Employee is added successfully";
}).WithParameterValidation();

app.Run();