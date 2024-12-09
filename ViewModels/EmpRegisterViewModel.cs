using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.ViewModels
{
    public class EmpRegisterViewModel
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salary { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }

        public int SelectedRoleId { get; set; } // Ensure this matches the type in your Employee model
        public List<SelectListItem> Roles { get; set; }
    }
}
