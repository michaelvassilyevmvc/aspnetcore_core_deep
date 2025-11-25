using DepartsCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepartsCrud.Pages.Shared.Components.EmployeeList;

public class EmployeeListViewComponent: ViewComponent
{
    private readonly IEmployeesRepository _employeesRepository;

    public EmployeeListViewComponent(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }
    public IViewComponentResult Invoke(string? filter, int? departmentId )
    {
        return View(_employeesRepository.GetEmployees(filter, departmentId));
    }
}