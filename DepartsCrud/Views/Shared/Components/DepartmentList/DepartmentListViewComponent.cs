using DepartsCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepartsCrud.Views.Shared.Components.DepartmentList;

[ViewComponent]
public class DepartmentListViewComponent : ViewComponent
{
    private readonly IDepartmentsRepository _departmentsRepository;

    public DepartmentListViewComponent(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }
    public IViewComponentResult Invoke(string? filter = null)
    {
        var departments = _departmentsRepository.GetDepartments(filter);
        return View(departments);
    }
}