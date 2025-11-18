using DepartsCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartsCrud.Pages.Employees;

public class Index : PageModel
{
    public List<Employee>? Employees { get; set; }

    public void OnGet()
    {
        // this.Employees = EmployeesRepository.GetEmployees();
    }

    public IActionResult OnGetSearchEmployeeResult(string? filter)
    {
        return ViewComponent("EmployeeList", new { filter });
    }
}