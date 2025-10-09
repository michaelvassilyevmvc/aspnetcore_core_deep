using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/register",
            (Registration reg) => { return $"User {reg.Email} is registrated successfully"; })
        .WithParameterValidation();

    endpoints.MapPost("/register",
            ([FromBody] Registration registration) =>
            {
                return $"User {registration.Email} is registered successfully";
            })
        .WithParameterValidation();
});


app.Run();

public class Registration
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 100 characters")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    public static ValueTask<Registration?> BindAsync(HttpContext httpContext)
    {
        string email = httpContext.Request.Query["email"];
        string password = httpContext.Request.Query["pwd1"];
        string confirmPassword = httpContext.Request.Query["pwd2"];
        return new ValueTask<Registration?>(new Registration
        {
            Email = email,
            Password = password,
            ConfirmPassword = confirmPassword
        });
    }
}