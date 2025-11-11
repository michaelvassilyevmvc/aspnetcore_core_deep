using System.ComponentModel.DataAnnotations;
using LearnValidationModel.SimpleMinimalAPI.Models;

namespace LearnValidationModel.SimpleMinimalApi;

public class Employee_EnsureSalary : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var employee = validationContext.ObjectInstance as Employee;
        if (employee is not null &&
            !string.IsNullOrWhiteSpace(employee.Position) &&
            employee.Position.Equals("Manager", StringComparison.InvariantCultureIgnoreCase)
           )
            if (employee.Salary < 100000)
                return new ValidationResult("A manager's salary has to be greater or equal to $100,000");

        return ValidationResult.Success;
    }
}