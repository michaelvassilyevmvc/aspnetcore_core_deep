using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnRazorPages.Pages.Employees;

public class Edit : PageModel
{
    [BindProperty(SupportsGet = true)] public string Id { get; set; }

    [BindProperty] public InputModel? InputModel { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPostSave()
    {
        if (!ModelState.IsValid)
        {
            InputModel.ErrorMessage = GetErrors();
            return Page();
        }

        return RedirectToPage("Index");
    }

    public void OnPostDelete()
    {
    }

    private List<string> GetErrors()
    {
        var errorMessages = new List<string>();
        foreach (var value in ModelState.Values)
        foreach (var error in value.Errors)
            errorMessages.Add(error.ErrorMessage);

        return errorMessages;
    }
}

public class InputModel
{
    public int? Id { get; set; }
    [Required] public string? Name { get; set; }

    public List<string>? ErrorMessage { get; set; }
}