using DepartsCrud.Helpers;
using DepartsCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepartsCrud.Controllers;

public class DepartmentsController : Controller
{
    private readonly IDepartmentsRepository _departmentsRepository;

    public DepartmentsController(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // [Route("/department-list/{filter?}")]
    // public IActionResult SearchDepartments(string? filter)
    // {
    //     var departments = DepartmentsRepository.GetDepartments(filter);
    //     return PartialView("_DepartmentList", departments);
    // }

    [Route("/department-list/{filter?}")]
    public IActionResult SearchDepartments(string? filter)
    {
        return ViewComponent("DepartmentList", new { filter });
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var department = _departmentsRepository.GetDepartmentById(id);
        if (department == null)
            return View("Error", new List<string>
            {
                "Department not found"
            });

        return View(department);
    }

    [HttpPost]
    public IActionResult Edit(Department department)
    {
        if (!ModelState.IsValid) return View("Error", ModelStateHelper.GetErrors(ModelState));

        _departmentsRepository.UpdateDepartment(department);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Department());
    }

    [HttpPost]
    public IActionResult Create(Department department)
    {
        if (!ModelState.IsValid) return View("Error", ModelStateHelper.GetErrors(ModelState));

        _departmentsRepository.AddDepartment(department);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var department = _departmentsRepository.GetDepartmentById(id);
        if (department is null)
        {
            ModelState.AddModelError("id", "Department not found");
            return View("Error", ModelStateHelper.GetErrors(ModelState));
        }

        _departmentsRepository.DeleteDepartment(department);
        return RedirectToAction(nameof(Index));
    }


}