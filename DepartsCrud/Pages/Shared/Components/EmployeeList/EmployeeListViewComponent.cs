using DepartsCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepartsCrud.Pages.Shared.Components.EmployeeList;

public class EmployeeListViewComponent: ViewComponent
{
    public IViewComponentResult Invoke(string? filter, int? departmentId )
    {
        return View(EmployeesRepository.GetEmployees(filter, departmentId));
    }
}