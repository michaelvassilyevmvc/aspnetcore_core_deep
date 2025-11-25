namespace DepartsCrud.Models;

public interface IDepartmentsRepository
{
    List<Department> GetDepartments(string? filter = null);
    Department? GetDepartmentById(int id);
    void AddDepartment(Department? Department);
    bool UpdateDepartment(Department? Department);
    bool DeleteDepartment(Department? Department);
}