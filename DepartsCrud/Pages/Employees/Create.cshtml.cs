using DepartsCrud.Helpers;
using DepartsCrud.Models;
using DepartsCrud.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepartsCrud.Pages.Employees;

public class Create : PageModel
{
    private readonly IDepartmentsRepository _departmentsRepository;
    private readonly IEmployeesRepository _employeesRepository;

    public Create(IDepartmentsRepository departmentsRepository, IEmployeesRepository employeesRepository)
    {
        _departmentsRepository = departmentsRepository;
        _employeesRepository = employeesRepository;
    }
    [BindProperty] public EmployeeViewModel? EmployeeViewModel { get; set; }

    public void OnGet()
    {
        this.EmployeeViewModel = new EmployeeViewModel();
        this.EmployeeViewModel.Employee = new Employee();
        this.EmployeeViewModel.Departments = _departmentsRepository.GetDepartments();
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
            _employeesRepository.AddEmployee(this.EmployeeViewModel.Employee);
        }

        return RedirectToPage("Index");
    }
}