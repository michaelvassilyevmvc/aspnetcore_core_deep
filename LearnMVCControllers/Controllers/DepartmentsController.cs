using Microsoft.AspNetCore.Mvc;

namespace LearnMVCControllers.Controllers;

public class DepartmentsController
{
    public string Index()
    {
        return "These are the departments";
    }

    public string Details(int? id)
    {
        return $"Department info: {id}";
    }

    [HttpPost]
    public string Create()
    {
        return "Created a new department";
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
}