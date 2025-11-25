using DepartsCrud.Helpers;
using DepartsCrud.Models;
using DepartsCrud.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartsCrud.Pages.Employees;

public class Edit : PageModel
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IDepartmentsRepository _departmentsRepository;

    public Edit(IEmployeesRepository employeesRepository, IDepartmentsRepository departmentsRepository)
    {
        _employeesRepository = employeesRepository;
        _departmentsRepository = departmentsRepository;
    }
    
    [BindProperty]
    public EmployeeViewModel? EmployeeViewModel { get; set; }
    public void OnGet(int id)
    {
        this.EmployeeViewModel = new EmployeeViewModel();
        this.EmployeeViewModel.Employee = _employeesRepository.GetEmployeeById(id);
        this.EmployeeViewModel.Departments = _departmentsRepository.GetDepartments();
        
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
            _employeesRepository.UpdateEmployee(EmployeeViewModel.Employee);
        }

        return RedirectToPage("Index");
    }
    
    public IActionResult OnPostDeleteEmployee(int id)
    {
        var employee = _employeesRepository.GetEmployeeById(id);
        if (employee == null)
        {
            ModelState.AddModelError("id", "Employee not found");
            var errors = ModelStateHelper.GetErrors(ModelState);
            return RedirectToPage("/Error", new { errors });
        }
        
        _employeesRepository.DeleteEmployee(employee);
        return RedirectToPage("Index");
    }
}