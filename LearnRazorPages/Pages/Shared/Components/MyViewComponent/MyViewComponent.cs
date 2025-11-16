using Microsoft.AspNetCore.Mvc;

namespace LearnRazorPages.Pages.Shared.Components.MyViewComponent;

[ViewComponent(Name = "MyViewComponent")]
public class MyViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}