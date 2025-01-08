
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }

        [Required(ErrorMessage = "Hospital name is required.")]
        [StringLength(100, ErrorMessage = "Hospital name cannot exceed 100 characters.")]
        public string HospitalName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [StringLength(20, ErrorMessage = "Contact number cannot exceed 20 characters.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Created date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Updated date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime? UpdatedAt { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        public Hospital(string hospitalName, string address, string contactNumber, DateTime createdAt, DateTime? updatedAt, string email)
        {
            HospitalName = hospitalName;
            Address = address;
            ContactNumber = contactNumber;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Email = email;
        }
    }
}

