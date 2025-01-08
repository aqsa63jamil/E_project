namespace WebApplication2.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    [Table("VisitRequest")]
    public class VisitRequest
    {
        [Key]
        [Column("VisitRequestId")]
        public int VisitRequestId { get; set; }

        [Required(ErrorMessage = "Hospital selection is required.")]
        [ForeignKey("Hospitals")]
        public int HospitalId { get; set; }

        [Required(ErrorMessage = "Reason is required.")]
        [MaxLength(255, ErrorMessage = "Reason cannot exceed 255 characters.")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        [RegularExpression("Pending|Approved|Rejected", ErrorMessage = "Status must be 'Pending', 'Approved', or 'Rejected'.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Request date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime? RequestedAt { get; set; } = DateTime.Now;

        [ForeignKey("FinanceManager")]
        [Required(ErrorMessage = "Finance Manager selection is required.")]
        public int? FinanceManagerId { get; set; }

        [Required]
        public int? EmpId { get; set; }

        [Required(ErrorMessage = "Employee CNIC is required.")]
        public int Emp_CNIC { get; set; }

        [Required]
        public string Emp_Email { get; set; }

        // For Dropdowns
        public static IEnumerable Hospitals { get; internal set; }
        public static IEnumerable FinanceManagers { get; internal set; }

        public void sentEmail(string email)
        {
            Emp_Email = email;
        }

        public VisitRequest() { }

    }

}
