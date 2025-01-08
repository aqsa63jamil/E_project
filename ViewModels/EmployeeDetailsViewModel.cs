using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ProfilePicture { get; set; }
        [Required]
        public string RoleName { get; set; }
        public int SelectedRoleId { get; set; }
        public int Status { get; set; } = 1;
        public string StatusDescription { get; set; }
        public int SelectedPolicyId { get; set; }
        public string PolicyName { get; set; }
        public decimal PolicyAmount { get; set; }
        public bool ShowStatusDropdown { get; set; }
        public decimal DeductAmount { get; set; }
        public decimal Remaining_Amount { get; set; }
        public DateTime? PolicyDate { get; set; } = new DateTime(2000, 1, 15, 0, 0, 0, 0);

        public DateTime RandomStartDate { get; set; }
    }
}
