using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class AdminLogin
    {
        [Key]
        public int? AdminId { get; set; }

        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        public string? Password { get; set; }

        public string ProfileImage { get; set; }
        public string Name { get; set; }

        public AdminLogin(string? email, string? password, string profileImage, string name)
        {
            Email = email;
            Password = password;
            ProfileImage = profileImage;
            Name = name;
        }

        public AdminLogin()
        {
        }
    }
}
