using Microsoft.AspNetCore.Mvc;

namespace LearnMVCControllers.Controllers;

[Route("/api")]
public class DepartmensController
{
    [HttpGet("departmens")]
    public string GetDepartments()
    {
        return "These are the departments";
    }

    [HttpGet("departmens/{id}")]
    public string GetDepartmentById(int id)
    {
        return $"Department info: {id}";
    }

    [NonAction]
    public string GetDepartmentByName(string name)
    {
        return "abc";
    }
}