using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {

        Connection db = new Connection();

        public IActionResult Dashboard()
        {
            return View();
        }
        //public ActionResult Create()
        //{
        //    var roles = db.Roles.Select(r => new SelectListItem
        //    {
        //        Value = r.RoleId.ToString(),
        //        Text = r.RoleName
        //    }).ToList();

        //    var viewModel = new EmpRegisterViewModel
        //    {
        //        Roles = roles
        //    };

        //    return View(viewModel);
        //}

        //[HttpPost]
        //public ActionResult Create(EmpRegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var empRegister = new Employee
        //        {
        //            RoleId = model.SelectedRoleId
        //        };

        //        db.EmpRegister.Add(empRegister);
        //        db.SaveChanges();

        //        // Redirect to a success page or action
        //        return RedirectToAction("EmployeeList");
        //    }

        //    // Re-fetch roles in case of validation errors
        //    model.Roles = db.Roles.Select(r => new SelectListItem
        //    {
        //        Value = r.RoleId.ToString(),
        //        Text = r.RoleName
        //    }).ToList();

        //    return View(model);
        //}

        // GET: List of Employees
        public ActionResult EmployeeList()
        {

            var employees = db.EmpRegisters.Select(e => new EmployeeDetailsViewModel
            {
                EmpId = e.EmpId,
                Name = e.Name,
                Email = e.Email,
                Password = e.Password,
                Salary = e.Salary,
                Address = e.Address,
                ContactNo = e.ContactNo,
                RoleName = e.Role.RoleName
            }).ToList();

            return View(employees);
        }

        public ActionResult AddEmployee()
        {
            var viewModel = new EmpRegisterViewModel
            {
                Roles = db.Roles.Select(r => new SelectListItem
                {
                    Value = r.RoleId.ToString(),
                    Text = r.RoleName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEmployee(EmpRegisterViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                // Log model data to debug
                Console.WriteLine($"Name: {model.Name}, RoleId: {model.SelectedRoleId}");

                // Hash the password
                var passwordHasher = new PasswordHasher<Employee>(); // Use Employee as the parameter for PasswordHasher
                var hashedPassword = passwordHasher.HashPassword(null, model.Password); // The 'null' represents the instance of Employee (not needed here)

                // Map ViewModel to Entity
                var employee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = hashedPassword, // Store the hashed password
                    Salary = model.Salary,
                    Address = model.Address,
                    ContactNo = model.ContactNo,
                    RoleId = model.SelectedRoleId // Foreign key
                };

                // Save to database
                db.EmpRegisters.Add(employee);
                db.SaveChanges();

                return RedirectToAction("EmployeeList");
            //}

            // Reload roles if the model state is invalid
            //model.Roles = db.Roles.Select(r => new SelectListItem
            //{
            //    Value = r.RoleId.ToString(),
            //    Text = r.RoleName
            //}).ToList();

            //return View(model);
        }


        public IActionResult Edit(int id)
        {
            var employee = db.EmpRegisters.Where(e => e.EmpId == id).Select(e => new EmployeeDetailsViewModel
            {
                EmpId = e.EmpId,
                Name = e.Name,
                Email = e.Email,
                Password = e.Password,
                Salary = e.Salary,
                Address = e.Address,
                ContactNo = e.ContactNo,
                RoleName = e.Role.RoleName,
                SelectedRoleId = e.RoleId // Use RoleId here
            }).FirstOrDefault();

            //if (employee == null)
            //{
            //    return NotFound();
            //}

            ViewBag.Roles = new SelectList(db.Roles, "RoleId", "RoleName", employee.SelectedRoleId);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeDetailsViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            // Find the existing employee by ID
            var employee = db.EmpRegisters.Find(model.EmpId);

            if (employee != null)
            {
                employee.EmpId = model.EmpId;
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Password = model.Password;
                employee.Salary = model.Salary;
                employee.Address = model.Address;
                employee.ContactNo = model.ContactNo;
                employee.RoleId = model.SelectedRoleId;

                // Save changes to the database
                db.SaveChanges();

                // Redirect to EmployeeList after successful update
                return RedirectToAction("EmployeeList");
            }
            //else
            //{
            //    // Handle case where employee is not found
            //    return NotFound();
            //}
            //}

            // Reload roles for dropdown and return the view if validation fails
            ViewBag.Roles = new SelectList(db.Roles, "RoleId", "RoleName", model.SelectedRoleId);
            return View(model); // Return the same view to fix validation errors
        }


        public IActionResult Delete(int id)
        {
            // Fetch the employee details from the database
            var employee = db.EmpRegisters
                .Include(e => e.Role) // Include Role details
                .FirstOrDefault(e => e.EmpId == id);

            if (employee != null)
            {
                // Map Employee to EmployeeDetailsViewModel
                var viewModel = new EmployeeDetailsViewModel
                {
                    EmpId = employee.EmpId,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Address = employee.Address,
                    ContactNo = employee.ContactNo,
                    RoleName = employee.Role?.RoleName // Handle null Role gracefully
                };

                return View(viewModel); // Pass the correct view model to the view
            }

            return NotFound();
        }


        //Delete POST
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = db.EmpRegisters.Find(id);
            if (employee != null)
            {
                db.EmpRegisters.Remove(employee);
                db.SaveChanges();
                return RedirectToAction("EmployeeList");
            }
            return NotFound();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(EmployeeLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verify the employee's credentials
                var employee = db.EmpRegisters
                    .Include(e => e.Role) // Include the Role navigation property
                    .FirstOrDefault(e => e.Email == model.Email && e.Password == model.Password);

                if (employee != null)
                {
                    // Create a session to store the employee's details
                    HttpContext.Session.SetString("EmployeeId", employee.EmpId.ToString());
                    HttpContext.Session.SetString("EmployeeRole", employee.Role.RoleName);

                    // Redirect to the profile page
                    return RedirectToAction("Profile");
                }
                else
                {
                    ViewBag.Error = "Invalid email or password.";
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Employee")]
        public ActionResult Profile()
        {
            var employeeId = HttpContext.Session.GetString("EmployeeId");

            if (!string.IsNullOrEmpty(employeeId))
            {
                // Fetch employee details
                var employee = db.EmpRegisters
                    .Include(e => e.Role) // Include Role information
                    .Where(e => e.EmpId.ToString() == employeeId)
                    .Select(e => new EmployeeDetailsViewModel
                    {
                        EmpId = e.EmpId,
                        Name = e.Name,
                        Email = e.Email,
                        Salary = e.Salary,
                        Address = e.Address,
                        ContactNo = e.ContactNo,
                        RoleName = e.Role.RoleName
                    })
                    .FirstOrDefault();

                if (employee != null)
                {
                    return View(employee);
                }
            }

            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
