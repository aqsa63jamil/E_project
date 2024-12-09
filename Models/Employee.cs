using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Employee
    {
        [Key]
            public int EmpId { get; set; }
        [Required]
            public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
            public string Salary { get; set; }
            public string Address { get; set; }
            public string ContactNo { get; set; }

        [ForeignKey("Roles")]
            public int RoleId { get; set; }
            public Role Role { get; set; } 
    }
}
