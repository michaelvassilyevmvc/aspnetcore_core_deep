using DepartsCrud.Helpers;
using DepartsCrud.Models;
using DepartsCrud.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartsCrud.Pages.Employees;

public class Edit : PageModel
{
    [BindProperty]
    public EmployeeViewModel? EmployeeViewModel { get; set; }
    public void OnGet(int id)
    {
        this.EmployeeViewModel = new EmployeeViewModel();
        this.EmployeeViewModel.Employee = EmployeesRepository.GetEmployeeById(id);
        this.EmployeeViewModel.Departments = DepartmentsRepository.GetDepartments();
        
    }   
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelStateHelper.GetErrors(ModelState);
            return RedirectToPage("/Error", new { errors });
        }

        if (EmployeeViewModel is not null && EmployeeViewModel.Employee is not null)
        {
            EmployeesRepository.UpdateEmployee(EmployeeViewModel.Employee);
        }

        return RedirectToPage("Index");
    }
    
    public IActionResult OnPostDeleteEmployee(int id)
    {
        var employee = EmployeesRepository.GetEmployeeById(id);
        if (employee == null)
        {
            ModelState.AddModelError("id", "Employee not found");
            var errors = ModelStateHelper.GetErrors(ModelState);
            return RedirectToPage("/Error", new { errors });
        }
        
        EmployeesRepository.DeleteEmployee(employee);
        return RedirectToPage("Index");
    }
}