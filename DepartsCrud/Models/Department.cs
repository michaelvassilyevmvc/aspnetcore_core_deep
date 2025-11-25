using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DepartsCrud.Models;

public class Department
{
    [HiddenInput]
    public int Id { get; set; }

    [Required] public string? Name { get; set; }

    [StringLength(500)] public string? Description { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    
    public Department()
    {
    }

    public Department(int id, string name, string? description = "")
    {
        Id = id;
        Name = name;
        Description = description;
    }

    
}