using System.ComponentModel.DataAnnotations;
using LearnValidationModel.SimpleMinimalAPI.Models;

namespace LearnValidationModel.SimpleMinimalApi;

public class MatchesPassword: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var auth = validationContext.ObjectInstance as Auth;
        if (auth is not null &&
            !string.IsNullOrWhiteSpace(auth.Password) &&
            !auth.Password.Equals(auth.ConfirmPassword))
        {
            return  new ValidationResult("Passwords do not match");
        }
        return ValidationResult.Success;
    }
}