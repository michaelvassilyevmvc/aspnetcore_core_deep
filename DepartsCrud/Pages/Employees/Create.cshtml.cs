using DepartsCrud.Helpers;
using DepartsCrud.Models;
using DepartsCrud.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartsCrud.Pages.Employees;

public class Create : PageModel
{
    [BindProperty] public EmployeeViewModel? EmployeeViewModel { get; set; }

    public void OnGet()
    {
        this.EmployeeViewModel = new EmployeeViewModel();
        this.EmployeeViewModel.Employee = new Employee();
        this.EmployeeViewModel.Departments = DepartmentsRepository.GetDepartments();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelStateHelper.GetErrors(ModelState);
            return RedirectToPage("/Error", new { errors });
        }

        if (this.EmployeeViewModel is not null && this.EmployeeViewModel.Employee is not null)
        {
            EmployeesRepository.AddEmployee(this.EmployeeViewModel.Employee);
        }

        return RedirectToPage("Index");
    }
}