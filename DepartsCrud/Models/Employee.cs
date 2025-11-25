using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DepartsCrud.Models;

public class Employee
{
    public Employee()
    {
    }

    public Employee(int id, string name, string position, double salary, int departmentId)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
        DepartmentId = departmentId;
    }

    [HiddenInput] public int Id { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public string? Position { get; set; }

    public double? Salary { get; set; }

    [Display(Name = "Department")]
    [Range(1, int.MaxValue, ErrorMessage = "Department is required")]
    public int DepartmentId { get; set; }

    public Department? Department { get; set; }
}