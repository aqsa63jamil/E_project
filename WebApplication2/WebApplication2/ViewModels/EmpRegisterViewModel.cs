using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.ViewModels
{
    public class EmpRegisterViewModel
    {
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
        public string ProfilePicture { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public int SelectedRoleId { get; set; }
        public string RoleName { get; set; }

        public int Status { get; set; } = 1;

        [Required(ErrorMessage = "Policy selection is required.")]
        public int SelectedPolicyId { get; set; }

        // List of Policies
        public List<SelectListItem> Policies { get; set; }
        // List of Roles
        public List<SelectListItem> Roles { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Remaining amount must be a positive number.")]
        public decimal Remaining_Amount { get; set; }
    }
}
