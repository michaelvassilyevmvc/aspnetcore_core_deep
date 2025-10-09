namespace LearnMinimalApiResult.Models;

public interface IEmployeesRepository
{
    List<Employee> GetEmployees();
    Employee? GetEmployeeById(int id);
    void AddEmployee(Employee? employee);
    bool UpdateEmployee(Employee? employee);
    bool DeleteEmployee(int id);
}