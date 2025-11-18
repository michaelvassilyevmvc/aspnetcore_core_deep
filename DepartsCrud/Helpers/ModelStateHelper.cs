using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DepartsCrud.Helpers;

public static class ModelStateHelper
{
    static public List<string> GetErrors(ModelStateDictionary modelState)
    {
        var errorMessages = new List<string>();
        foreach (var value in modelState.Values)
        foreach (var error in value.Errors)
            errorMessages.Add(error.ErrorMessage);

        return errorMessages;
    }
}