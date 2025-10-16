using LearnMVCControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnMVCControllers.Controllers;

public class DepartmentsController : Controller
{
    public IActionResult Index()
    {
        return Content
        (
            "<h1>Departments</h1>Welcome to website",
            "text/html"
        );
    }

    public object Details(int id)
    {
        return RedirectToAction(nameof(EmployeesController.GetEmployeesByDepartment),
            nameof(EmployeesController)
                .Replace("Controller", ""), new { id = id });

        // return LocalRedirect($"/employees/GetEmployeesByDepartment/{id}");

        // return Redirect("https://google.com");

        // return new RedirectResult("https://www.google.com");

        // return new LocalRedirectResult($"/employees/GetEmployeesByDepartment/{id}");

        // return new RedirectToActionResult(
        //     "GetEmployeesByDepartment",
        //     "Employees",
        //     new { id = id });


        // return Json(new Department()
        // {
        //     Id = 11,
        //     Name = "Sales"
        // });


        // return $"Department info: {id}";
        // return new Department()
        // {
        //     Id =  11,
        //     Name = "Sales"
        // };
    }

    [HttpPost("create")]
    public object Create([FromBody] Department department)
    {
        ModelState.AddModelError("Description", "Description is required");

        foreach (var value in ModelState.Values)
        {
            foreach (var error in value.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }

        return department;
    }

    [HttpPost]
    public string Delete(int? id)
    {
        return $"Deleting department: {id}";
    }

    [HttpPost]
    public string Edit(int? id)
    {
        return $"Updating department: {id}";
    }

    [Route("/download_vf")]
    public IActionResult ReturnVirtualFile()
    {
        return File("/readme.txt", "application/octet-stream");
    }

    [Route("/download_pf")]
    public IActionResult ReturnPhysicalFile()
    {
        return PhysicalFile("c:/temp/somefile.pdf", "application/pdf");
    }

    [Route("/download_cf")]
    public IActionResult ReturnContentFile()
    {
        byte[] bytes = System.IO.File.ReadAllBytes("c:/temp/somefile.pdf");
        return File(bytes, "application/pdf");
    }
}