using System.ComponentModel.DataAnnotations;
using System.Numerics;


namespace WebApplication2.Models
{
    public class Per_Hospital
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TotalBeds { get; set; }
        [Required]
        public int ActiveBeds { get; set; }
        [Required]
        public int TotalDoctors { get; set; }
        [Required]
        public int TotalNurses{ get; set; }
        public Per_Hospital() { }
    }
}
