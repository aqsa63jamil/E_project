using X.PagedList;

namespace WebApplication2.Models
{
    public class DashboardViewModel
    {
      
        public int TotalBeds { get; set; }
        public int ActiveBeds { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalNurses { get; set; }
        public List<Notification> Notifications { get; set; }
        public IEnumerable<Payment_Data> Payment { get; set; }
      


    }


}
