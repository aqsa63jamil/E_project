using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public Role() { }

        public Role(string roleName)
        {
            RoleName = roleName;
        }

        public ICollection<Employee> EmpRegisters { get; set; }

    }
}
