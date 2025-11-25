using DepartsCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartsCrud.Pages.Employees;

public class DepartmentEmployees : PageModel
{
    private readonly IDepartmentsRepository _departmentsRepository;

    public DepartmentEmployees(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }
    public string? DepartmentName { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? DepartmentId { get; set; }
    
    public void OnGet(int? departmentId)
    {
        if (departmentId.HasValue)
        {
            var department = _departmentsRepository.GetDepartmentById(DepartmentId.Value);
            DepartmentName = department?.Name;
        }
    }
}