using DepartsCrud.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartsCrud.Pages.Employees;

public class Index : PageModel
{
    public List<Employee>? Employees { get; set; }
    public void OnGet()
    {
        this.Employees = EmployeesRepository.GetEmployees();
        
    }
}