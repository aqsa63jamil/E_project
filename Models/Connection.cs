using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class Connection : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=project;User Id=sa;Password=useradmin;TrustServerCertificate=True");
        }

        public DbSet<Employee> EmpRegisters { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
