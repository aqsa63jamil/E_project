using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using WebApplication2.Migrations;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class Medical_Invoice
    {
        [Key]
        public int MedicalId { get; set; }

        [Required]
        public int? EmpId { get; set; }

        [Required]
        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        public decimal TotalAmount { get; set; }

        public string Desc { get; set; }

        public string Emp_Name { get; set; }
        public string Emp_Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;


        public Role Role { get; set; }

        // Parameterless constructor for EF Core
        public Medical_Invoice() { }

        // Optional method to set additional properties (like Emp_Name, Emp_Email) after the object is created
        public void Medical_InvoiceDetails(string empName, string empEmail, DateTime createdate)
        {
            Emp_Name = empName;
            Emp_Email = empEmail;
            CreateDate = createdate;
        }
    }
}



