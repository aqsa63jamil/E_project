using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Policy
    {
        [Key]
        public int PolicyId { get; set; }
        [Required]
        [StringLength(100)]
        public string PolicyName { get; set; }

        [Required]
        [StringLength(500)]
        public string PolicyDesc { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Policy amount must be greater than zero.")]
        public decimal PolicyAmount { get; set; }

        public ICollection<Employee> Policies { get; set; }
    }
}
