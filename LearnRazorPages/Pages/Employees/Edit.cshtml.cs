using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnRazorPages.Pages.Employees;

public class Edit : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Id { get; set; }
    public void OnGet(string id)
    {
        
    }
}