namespace WebApplication2.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public int EmpId { get; set; } // Primary key of the employee
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salary { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string RoleName { get; set; }
        public int SelectedRoleId { get; set; }
    }
}
