using System.ComponentModel.DataAnnotations;

namespace LearnMinimalApiResult.Models;

public class Employee
{
    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    [Required] public string Position { get; set; }

    public double Salary { get; set; }
}