namespace DepartsCrud.Models;

public interface IEmployeesRepository
{
    List<Employee> GetEmployees(string? filter = null, int? departmentId = null);
    Employee? GetEmployeeById(int id);
    void AddEmployee(Employee? employee);
    bool UpdateEmployee(Employee? employee);
    bool DeleteEmployee(Employee? employee);
}