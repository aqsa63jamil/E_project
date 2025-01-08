
namespace WebApplication2.Models
{
    using Azure.Core;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;



    public class TreatmentRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Hospital selection is required.")]
        [ForeignKey("Hospitals")]
        public int HospitalId { get; set; }

        [Required(ErrorMessage = "Finance Manager selection is required.")]
        [ForeignKey("FinanceManagers")]
        public int? FinanceManagerId { get; set; }

        [Required(ErrorMessage = "Treatment details are required.")]
        [MaxLength(500, ErrorMessage = "Treatment details cannot exceed 500 characters.")]
        public string TreatmentDetails { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Pending|Approved|Rejected", ErrorMessage = "Status must be either 'Pending', 'Approved', or 'Rejected'.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Request date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? RequestDate { get; set; }

        [Required(ErrorMessage = "Approval date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? ApprovalDate { get; set; }

        [Required(ErrorMessage = "Employee selection is required.")]
        public int? EmpId { get; set; }

        [Required]
        public string Emp_Email { get; set; }

        public List<Notification> Notifications { get; set; }

        public void sentEmail(string email)
        {
            Emp_Email = email;
        }

        public TreatmentRequest() { }


        public TreatmentRequest(int? empId, int hospitalId, int? financeManagerId, string treatmentDetails, string status, DateTime? requestDate, DateTime? approvalDate, List<Notification> notifications, string email)
        {
            EmpId = empId;
            HospitalId = hospitalId;
            FinanceManagerId = financeManagerId;
            TreatmentDetails = treatmentDetails;
            Status = status;
            RequestDate = requestDate;
            ApprovalDate = approvalDate;
            Notifications = notifications;
            Emp_Email = email;
        }
    }

}


