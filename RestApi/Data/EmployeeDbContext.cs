using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Data;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {
    }

    public EmployeeDbContext() { }
    public DbSet<Employee> Employees { get; set; }
}
