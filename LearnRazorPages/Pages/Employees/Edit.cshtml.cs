using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnRazorPages.Pages.Employees;


public class Edit : PageModel
{
    [BindProperty(SupportsGet = true)] public string Id { get; set; }

    [BindProperty]
    public InputModel? InputModel { get; set; }
    public void OnGet()
    {
    }

    public void OnPostSave()
    {
    }

    public void OnPostDelete()
    {
    }
}

public class InputModel
{
    public int? Id { get; set; }
    public string? Name { get; set; }
}