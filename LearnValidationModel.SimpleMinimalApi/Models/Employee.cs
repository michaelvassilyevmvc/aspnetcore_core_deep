using System.ComponentModel.DataAnnotations;
using LearnValidationModel.SimpleMinimalApi;

namespace LearnValidationModel.SimpleMinimalAPI.Models;

public class Employee
{
    public int Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Position { get; set; }

    [Required]
    [Range(50000, 200000)]
    [Employee_EnsureSalary]
    public decimal Salary { get; set; }

    public Employee(int id, string name, string position, decimal salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }
}