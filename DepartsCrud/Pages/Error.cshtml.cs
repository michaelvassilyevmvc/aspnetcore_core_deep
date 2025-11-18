using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartsCrud.Pages;

public class Error : PageModel
{
    public List<string>? Errors { get; set; }
    public void OnGet(List<string> errors)
    {
        Errors = errors ?? new List<string>();
    }
    
    
}