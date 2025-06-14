using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.DataContext
{
    public class NewGenWebSoftechDb:DbContext
    {
        public NewGenWebSoftechDb(DbContextOptions options):base(options)
        { 
            
        }
        public DbSet<Department> Department { get; set; } 
        public DbSet<Employee> Employee { get; set; } 

        //DbSet<Department> Departments { get; set; }
        //DbSet<Employee> Employees { get; set; }
    }
}
