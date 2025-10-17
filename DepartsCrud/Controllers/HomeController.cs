using Microsoft.AspNetCore.Mvc;

namespace DepartsCrud.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Content("Welcome to the departments management.");
    }
}