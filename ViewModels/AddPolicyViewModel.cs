using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class AddPolicyViewModel
    {
        [Key]
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDesc { get; set; }
        public decimal PolicyAmount { get; set; }
    }
}
