using Microsoft.AspNetCore.Mvc;

namespace LearnMVCControllers.Controllers;

public class EmployeesController : Controller
{
    public IActionResult GetEmployeesByDepartment(
        [FromRoute(Name = "id")] int departmentId
    )
    {
        return Content($"Loading employees under department {departmentId}");
    }
}