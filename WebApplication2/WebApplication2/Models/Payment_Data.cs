namespace WebApplication2.Models
{
    public class Payment_Data
    {

        public decimal TotalAmount { get; set; }

        public string Desc { get; set; }

        public string Emp_Name { get; set; }
        public string Emp_Email { get; set; }
        public DateTime CreateDate { get; set; }  = DateTime.Now;
    }
}
