
namespace WebApplication2.Models
{
    using WebApplication2.Models;
    using Microsoft.AspNetCore.Http.HttpResults;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FinanceManager
    {
        [Key]
        public int FinanceManagerId { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        public string Email { get; set; }

        public FinanceManager(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }
    }
}

