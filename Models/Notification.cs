using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

      
        [ForeignKey("TreatmentRequest")]
        public int TreatmentRequestId { get; set; }

        public TreatmentRequest TreatmentRequest { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

       public Notification() { }    
    }

}
