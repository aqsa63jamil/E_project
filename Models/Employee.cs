using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string ContactNo { get; set; }

        public string ProfilePicture { get; set;}

        [ForeignKey("Roles")]
        [Required(ErrorMessage = "Role is required")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int Status { get; set; } = 1;

        [ForeignKey("Policies")]
        [Required(ErrorMessage = "Policy is required")]
        public int PolicyId { get; set; }
        public Policy Policy { get; set; }
        public decimal Remaining_Amount { get; set; }
        public DateTime? PolicyDate { get; set; } = new DateTime(2000, 1, 15, 0, 0, 0, 0);

    }
}
