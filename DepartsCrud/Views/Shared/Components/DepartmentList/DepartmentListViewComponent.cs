using DepartsCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepartsCrud.Views.Shared.Components.DepartmentList;

[ViewComponent]
public class DepartmentListViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var departments = DepartmentsRepository.GetDepartments();
        return View(departments);
    }
}