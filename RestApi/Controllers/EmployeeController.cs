using Microsoft.AspNetCore.Mvc;
using RestApi.Data;
using RestApi.Models;

namespace RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeDbContext _context;

    public EmployeeController(EmployeeDbContext context)
    {
        _context = context;
    }


    [HttpGet("all")]
    public IActionResult GetEmployees()
    {
        var res = _context.Employees.Select(x => new EmployeeDTO
        {
            Name = x.Name,
            Age = x.Age,
            Description = x.Description,
        }).ToList();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
        var res = _context.Employees.Where(x => x.Id == id).Select(x => new EmployeeDTO
        {
            Name = x.Name,
            Age = x.Age,
            Description = x.Description,
        }).FirstOrDefault();
        if (res is null)
        {
            return NotFound($"User with id {id} is not found");
        }
        return Ok(res); 
    }

    [HttpPost("add")]
    public IActionResult AddEmployee(EmployeeDTO model)
    {
        _context.Employees.Add(new Employee
        {
            Description = model.Description,
            Name = model.Name,
            Age = model.Age,
        });
        _context.SaveChanges();
        return Created($"https://localhost:7281/api/Employee/{3}", model);
    }


    [HttpDelete("delete/{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
        _context.Employees.Remove(employee);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut("update/{id}")]
    public IActionResult DeleteEmployee(int id, EmployeeDTO model)
    {
        var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
        employee.Name = model.Name;
        _context.SaveChanges();
        return Ok();
    }
}