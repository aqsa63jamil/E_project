using System.Data;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {   
      Connection db = new Connection();

        public ActionResult  Dashboard()
        {
            if (HttpContext.Session.GetString("AdminId") != null)
            {

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminProfileImage = HttpContext.Session.GetString("AdminProfileImage");

            HttpContext.Session.SetInt32("FinanceCount", db.EmpRegisters.Count(e => e.Role.RoleName == "Finance Manager"));
            HttpContext.Session.SetInt32("ManagerCount", db.EmpRegisters.Count(e => e.Role.RoleName == "Manager"));
            HttpContext.Session.SetInt32("EmployeeCount", db.EmpRegisters.Count(e => e.Role.RoleName == "Employee"));
            HttpContext.Session.SetInt32("InactiveEmployeesCount", db.EmpRegisters.Count(e => e.Status == 0));
            HttpContext.Session.SetInt32("TotalEmployees", db.EmpRegisters.Count());
            HttpContext.Session.SetInt32("TotalPolicies", db.Policies.Count());
            HttpContext.Session.SetInt32("TotalRoles", db.Roles.Count());

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        private void UpdateDashboardStatistics()
        {
            HttpContext.Session.SetInt32("FinanceCount", db.EmpRegisters.Count(e => e.Role.RoleName == "Finance Manager"));
            HttpContext.Session.SetInt32("ManagerCount", db.EmpRegisters.Count(e => e.Role.RoleName == "Manager"));
            HttpContext.Session.SetInt32("EmployeeCount", db.EmpRegisters.Count(e => e.Role.RoleName == "Employee"));
            HttpContext.Session.SetInt32("InactiveEmployeesCount", db.EmpRegisters.Count(e => e.Status == 0));
            HttpContext.Session.SetInt32("TotalEmployees", db.EmpRegisters.Count());
            HttpContext.Session.SetInt32("TotalPolicies", db.Policies.Count());
            HttpContext.Session.SetInt32("TotalRoles", db.Roles.Count());
        }

        //Search
        public IActionResult DashboardSearch(string search, int? page)
        {
            var employees = db.EmpRegisters.Include(e => e.Role).Include(e => e.Policy).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                employees = employees.Where(e =>
                e.Name.ToLower().Contains(search) ||
                e.Role.RoleName.ToLower().Contains(search) ||
                e.Policy.PolicyName.ToLower().Contains(search) ||
                (search == "active" && e.Status == 1) ||
                (search == "inactive" && e.Status == 0));
            }

            var employeeList = employees.Select(e => new EmployeeDetailsViewModel
            {
                EmpId = e.EmpId,
                Name = e.Name,
                Email = e.Email,
                Salary = e.Salary,
                Address = e.Address,
                ContactNo = e.ContactNo,
                RoleName = e.Role.RoleName,
                Status = e.Status,
                PolicyName = e.Policy.PolicyName,
                PolicyAmount = e.Policy.PolicyAmount,
                Remaining_Amount = e.Remaining_Amount,
                PolicyDate = e.PolicyDate
            }).OrderBy(e => e.EmpId);

            ViewBag.GlobalSearchQuery = search;
            return View("EmployeeList", employeeList); 
        }

        //Employees list
        public ActionResult EmployeeList(int page = 1)
        {
            int pageSize = 5;
            int skip = (page - 1) * pageSize;
           
            var employees = db.EmpRegisters.Include(e => e.Role).Include(e => e.Policy).Where(e => e.Status == 1)
            .Select(e => new EmployeeDetailsViewModel
            {
                 EmpId = e.EmpId,
                 Name = e.Name,
                 Email = e.Email,
                 Salary = e.Salary,
                 Address = e.Address,
                 ContactNo = e.ContactNo,
                 RoleName = e.Role.RoleName,
                 StatusDescription = e.Status == 1 ? "Active" : "Inactive",
                 PolicyName = e.Policy.PolicyName,
                 PolicyAmount = e.Policy.PolicyAmount,
                 Remaining_Amount = e.Remaining_Amount,
                 PolicyDate = e.PolicyDate
            }).Skip(skip).Take(pageSize).ToList();

            int totalRecords = db.EmpRegisters.Count(e => e.Status == 1);
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            ViewBag.Employees = employees;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

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
        [Route("Admin/AddEmployee")]
        public ActionResult AddEmployee(EmpRegisterViewModel model, string returnUrl)
        {
            if (db.EmpRegisters.Any(e => e.Email == model.Email))
            {
                ModelState.AddModelError("Email", "The email address already exists. Please use a different email address.");
                model.Roles = db.Roles.Select(r => new SelectListItem
                {
                    Value = r.RoleId.ToString(),
                    Text = r.RoleName
                }).ToList();
                return View(model);
            }

            var passwordHasher = new PasswordHasher<Employee>();
            var hashedPassword = passwordHasher.HashPassword(null, model.Password);

            var policy = db.Policies.FirstOrDefault(p => p.PolicyId == model.SelectedPolicyId);

            int assignedPolicyId;

            //automatically assigned premium policy to managers
            var selectedRoleName = db.Roles.FirstOrDefault(r => r.RoleId == model.SelectedRoleId)?.RoleName;

            if(selectedRoleName == "Manager")
            {
                assignedPolicyId = 3; //premium policy
            }
            else
            {
                assignedPolicyId = 1; //default policy
            }

            var remaining = db.Policies.Where(x => x.PolicyId == assignedPolicyId).FirstOrDefault();

            var randomStartDate = DateTime.Now.AddDays(-new Random().Next(0, 30));
            TempData["RandomStartDate"] = randomStartDate.ToString("yyyy-MM-dd");

            var employee = new Employee
            {
                Name = model.Name,
                Email = model.Email,
                Password = hashedPassword,
                Salary = model.Salary,
                Address = model.Address,
                ContactNo = model.ContactNo,
                RoleId = model.SelectedRoleId,
                Status = model.Status,
                PolicyId = assignedPolicyId,
                Remaining_Amount = remaining.PolicyAmount
            };

            db.EmpRegisters.Add(employee);
            db.SaveChanges();
            TempData["SuccessMessage"] = $"Employee (<b>{employee.Name}</b>) was successfully added.";

            if (selectedRoleName == "Manager")
            {
                return RedirectToAction("ManagerDetails", "Admin"); 
            }
            else if (selectedRoleName == "Employee")
            {
                return RedirectToAction("EmployeeDetails", "Admin"); 
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add the employee. Please check the details and try again.";
                return RedirectToAction("EmployeeList", "Admin");

            }
          
            //return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("EmployeeList");
        }

        public IActionResult Edit(int id, string returnUrl)
        {
            var employee = db.EmpRegisters.Where(e => e.EmpId == id).Select(e => new EmployeeDetailsViewModel
            {
                EmpId = e.EmpId,
                Name = e.Name,
                Email = e.Email,
                Salary = e.Salary,
                Address = e.Address,
                ContactNo = e.ContactNo,
                RoleName = e.Role.RoleName,
                SelectedRoleId = e.RoleId,
                Status = e.Status,
                ShowStatusDropdown = e.Status == 0,
                SelectedPolicyId = e.PolicyId,
                PolicyAmount = e.Policy.PolicyAmount,
                PolicyDate = e.PolicyDate,
                DeductAmount = 0
            }).FirstOrDefault();
          
            ViewBag.Policies = new SelectList(db.Policies, "PolicyId", "PolicyName", employee.SelectedPolicyId);

            var policy = db.Policies.FirstOrDefault(p => p.PolicyId == employee.SelectedPolicyId);
            ViewBag.PolicyAmount = policy?.PolicyAmount ?? 0;

            ViewBag.ReturnUrl = returnUrl;

            ViewBag.StatusOption = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Active" },
                new SelectListItem { Value = "0", Text = "Inactive" }
            };

            ViewBag.Roles = new SelectList(db.Roles, "RoleId", "RoleName", employee.SelectedRoleId);
           
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeDetailsViewModel model, string returnUrl)
        {
            var employee = db.EmpRegisters.Find(model.EmpId);
            
            if (employee != null)
            {
                var policy = db.Policies.FirstOrDefault(p => p.PolicyId == model.SelectedPolicyId);
               
                if(employee.RoleId != 1)
                {
                    employee.EmpId = model.EmpId;
                    employee.Name = model.Name;
                    employee.Email = model.Email;
                    employee.Salary = model.Salary;
                    employee.Address = model.Address;
                    employee.ContactNo = model.ContactNo;
                    employee.RoleId = model.SelectedRoleId;
                    employee.Status = model.Status;
                    employee.PolicyId = model.SelectedPolicyId;
                    employee.Remaining_Amount = employee.Remaining_Amount - model.DeductAmount;

                }
                else
                {
                    employee.EmpId = model.EmpId;
                    employee.Name = model.Name;
                    employee.Email = model.Email;
                    employee.Salary = model.Salary;
                    employee.Address = model.Address;
                    employee.ContactNo = model.ContactNo;
                    employee.RoleId = model.SelectedRoleId;
                    employee.Status = model.Status;
                    employee.PolicyId = model.SelectedPolicyId;

                    
                    if(model.DeductAmount != 0)
                    {
                        employee.PolicyDate = employee.PolicyDate;
                    employee.Remaining_Amount = employee.Remaining_Amount - model.DeductAmount;
                        TempData["SuccessMessage"] = $"Amount deduction for employee (<b>{employee.Name}</b>) was processed successfully.";
                    }
                    else if(model.SelectedPolicyId != 1)
                    {
                        employee.PolicyDate = DateTime.Now;
                        employee.Remaining_Amount = policy.PolicyAmount;
                        TempData["SuccessMessage"] = $"Amount deduction for employee (<b>{employee.Name}</b>) was processed successfully.";
                    }
                    else
                    {
                        employee.PolicyDate = DateTime.Now;
                        employee.Remaining_Amount = policy.PolicyAmount;
                        TempData["SuccessMessage"] = $"Policy for employee (<b>{employee.Name}</b>) was successfully updated.";
                    }
                   
                }

                db.SaveChanges();
                UpdateDashboardStatistics();
                return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("EmployeeList");
            }
            TempData["ErrorMessage"] = $"Failed to update details for employee (<b>{model.Name}</b>). Please try again.";
            ViewBag.Policies = new SelectList(db.Policies, "PolicyId", "PolicyName", model.SelectedPolicyId); 
            
            ViewBag.StatusOption = new List<SelectListItem>
            { 
                new SelectListItem { Value = "1", Text = "Active"},
                new SelectListItem { Value = "0", Text = "Inacive"}
            };
            return View(employee);
        }

        public JsonResult GetPolicyAmount(int policyId)
        {
            var policy = db.Policies.FirstOrDefault(p => p.PolicyId == policyId);
            if (policy != null)
            {
                return Json(new { policyAmount = policy.PolicyAmount });
            }
            return Json(new { policyAmount = 0 });
        }

        public IActionResult Delete(int id, string returnUrl, bool isDeactivating = false)
        {
            var employee = db.EmpRegisters.Include(e => e.Role).Include(e => e.Policy).FirstOrDefault(e => e.EmpId == id);

            if (employee != null)
            {
                var viewModel = new EmployeeDetailsViewModel
                {
                    EmpId = employee.EmpId,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Address = employee.Address,
                    ContactNo = employee.ContactNo,
                    RoleName = employee.Role?.RoleName,
                    Status = employee.Status,
                    PolicyName = employee.Policy?.PolicyName,
                    PolicyAmount = employee.Policy.PolicyAmount
                };
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.IsDeactivating = isDeactivating;
                return View(viewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id, string returnUrl)
        {
            var employee = db.EmpRegisters.Find(id);
            if (employee != null)
            {
                if (employee.Status == 0)
                {
                    db.EmpRegisters.Remove(employee);
                    TempData["SuccessMessage"] = $"Employee (<b>{employee.Name}</b>) was permanently deleted.";
                }
                else
                {
                    employee.Status = 0;
                    employee.PolicyId = 1;
                    employee.PolicyDate = DateTime.Now;
                    TempData["SuccessMessage"] = $"Employee (<b>{employee.Name}</b>) was successfully deactivated.";
                }

                db.SaveChanges();

                return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("EmployeeList");
            }

            TempData["ErrorMessage"] = "Failed to delete the employee. Employee not found.";
            return NotFound();
        }

        //  Update Employee
        public IActionResult UpdateEmployee()
        {
   
            return View();
        }

        [HttpPost]
        public IActionResult UpdateEmployee(int search)
        {
            var employee = db.EmpRegisters
                .Include(e => e.Policy)
                .Include(e => e.Role) 
                .FirstOrDefault(e => e.EmpId == search);

            if (employee == null)
            {
                ViewBag.Message = "Employee not found.";
                return View("Error");
            }

            var employeeDetails = new EmployeeDetailsViewModel
            {
                EmpId = employee.EmpId,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                Address = employee.Address,
                ContactNo = employee.ContactNo,
                RoleName = employee.Role.RoleName,
                Status = employee.Status,
                PolicyName = employee.Policy?.PolicyName,
                PolicyAmount = employee.Policy.PolicyAmount,
                Remaining_Amount = employee.Remaining_Amount,
                PolicyDate = employee.PolicyDate
            };

            return View(employeeDetails);
        }

        [HttpPost]
        public IActionResult UpdateEmployeeRemainingAmount(int employeeId)
        {
            var employee = db.EmpRegisters.Include(e => e.Policy).FirstOrDefault(e => e.EmpId == employeeId);

            if (employee != null && employee.Policy != null)
            {
                if (employee.PolicyDate.HasValue && (DateTime.Now - employee.PolicyDate.Value).TotalDays < 365)
                {
                    TempData["Error"] = $"Cannot update. Policy for (<b>{employee.Name}</b>) was assigned less than one year ago.";
                }
                else
                {
                    employee.Remaining_Amount = employee.Policy.PolicyAmount;
                    employee.PolicyDate = DateTime.Now;
                    db.SaveChanges();
                    TempData["Message"] = $"Successfully updated remaining amount for (<b>{employee.Name}</b>).";
                }
            }
            else
            {
                TempData["Error"] = "Failed to update. Employee or policy not found.";
            }

            return RedirectToAction("UpdateEmployee");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var admin = db.Admin.FirstOrDefault(a => a.Email == email && a.Password == password);

            if ( admin != null)
            {
                HttpContext.Session.SetString("AdminId", admin.AdminId.ToString());
                HttpContext.Session.SetString("AdminName", admin.Name);
                HttpContext.Session.SetString("AdminEmail", admin.Email);
                string profileImagePath = !string.IsNullOrEmpty(admin.ProfileImage)
             ? admin.ProfileImage
             : "/img/default-profile.png";

                HttpContext.Session.SetString("AdminProfileImage", profileImagePath);

                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SendOTP(string email)
        {
            Random random = new Random();
            var code = random.Next(000000, 999999);

            HttpContext.Session.SetInt32("code", code);

            var admin_id = db.Admin.Where(X => X.Email == email).FirstOrDefault();
            if (admin_id?.AdminId != null)
            {
                HttpContext.Session.SetInt32("adminid", admin_id.AdminId.Value); 
            }
            sendemail(email, $"Reset Password", $"there is your OTP{code}");
            return RedirectToAction("VerifyOTP");
        }

        public void sendemail(string to, string body, string sub)
        {
            MailMessage msg = new MailMessage("aqsajamil379@gmail.com", to, sub, body);
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("aqsajamil379@gmail.com", "lcljeblcsrlfxyoi"),
                EnableSsl = true,
            };
            client.Send(msg);
        }

        public IActionResult VerifyOTP()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyOTP(int code)
        {
            var code_session = HttpContext.Session.GetInt32("code");

            if (code == code_session)
            {
                return RedirectToAction("ResetPassword");
            }

            TempData["error"] = "Wrong otp";
            return View();

        }
      
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            var id = HttpContext.Session.GetInt32("adminid");
            var data = db.Admin.Where(x => x.AdminId == id).FirstOrDefault();

            data.Password = newPassword;

            db.SaveChanges();
            TempData["change"] = "Your password has been changed now you login with new password";
            return RedirectToAction("Login");
           
        }

        public IActionResult NotActive()
        {
            return View();
        }

        public ActionResult FinanceManagerProfile()
        {
            var employeeId = HttpContext.Session.GetString("EmployeeId");
            var employeeRole = HttpContext.Session.GetString("EmployeeRole");

            if (employeeRole != "Finance Manager")
            {
                return RedirectToAction("AccessDenied");
            }

            var employee = GetEmployeeDetails(employeeId);

            if (employee != null)
            {
                return View(employee);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManagerProfile()
        {
            var employeeId = HttpContext.Session.GetString("EmployeeId");
            var employeeRole = HttpContext.Session.GetString("EmployeeRole");

            if (employeeRole != "Manager")
            {
                return RedirectToAction("AccessDenied");
            }

            var employee = GetEmployeeDetails(employeeId);
            if (employee != null)
            {
                return View(employee);
            }

            return RedirectToAction("Login");
        }

        public ActionResult EmployeeDetails(int page = 1)
        {
            int pageSize = 5;
            int skip = (page - 1) * pageSize;

            var activeemployees = db.EmpRegisters
                .Include(e => e.Role).Include(e => e.Policy)
                .Where(e => e.Role.RoleName == "Employee" && e.Status == 1).Select(e => new EmployeeDetailsViewModel
                {
                   EmpId = e.EmpId,
                   Name = e.Name,
                   Email = e.Email,
                   Salary = e.Salary,
                   ContactNo = e.ContactNo,
                   Address = e.Address,
                   RoleName = e.Role.RoleName,
                   StatusDescription = e.Status == 1 ? "Active" : "Inactive",
                   PolicyName = e.Policy.PolicyName,
                   PolicyAmount = e.Policy.PolicyAmount,
                   Remaining_Amount = e.Remaining_Amount,
                   PolicyDate = e.PolicyDate
                }).Skip(skip).Take(pageSize).ToList();
            
            int totalRecords = db.EmpRegisters.Count(e => e.Role.RoleName == "Employee" && e.Status == 1);
            int totalPages = totalRecords > 0 ? (int)Math.Ceiling((double)totalRecords / pageSize) : 1;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View("RoleBasedDetails", activeemployees);
        }

        public IActionResult InactiveEmployees(int page = 1)
        {
            int pageSize = 5;
            int skip = (page - 1) * pageSize;

            var inactiveemployees = db.EmpRegisters.Include(e => e.Role).Include(e => e.Policy).Where(e => e.Status == 0)
            .Select(e => new EmployeeDetailsViewModel
            { 
                EmpId = e.EmpId,
                Name = e.Name,
                Email = e.Email,
                Salary = e.Salary,
                Address = e.Address,
                ContactNo = e.ContactNo,
                RoleName = e.Role.RoleName,
                StatusDescription = e.Status == 0 ? "Inactive" : "Active",
                SelectedPolicyId = e.PolicyId,
                PolicyName = e.Policy.PolicyName,
                PolicyAmount = e.Policy.PolicyAmount,
                Remaining_Amount = e.Remaining_Amount,
                PolicyDate = e.PolicyDate
            }).Skip(skip).Take(pageSize).ToList();

            int totalRecords = db.EmpRegisters.Count(e => e.Status == 0);
            int totalPages = totalRecords > 0 ? (int)Math.Ceiling((double)totalRecords / pageSize) : 1;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(inactiveemployees);
        }

        public ActionResult FinanceDetails(int page = 1)
        {
            int pageSize = 5;
            int skip = (page - 1) * pageSize;

            var financeManagers = db.EmpRegisters.Include(e => e.Role).Include(e => e.Policy).Where(e => e.Role.RoleName == "Finance Manager" && e.Status == 1)
            .Select(e => new EmployeeDetailsViewModel
            {
                EmpId = e.EmpId,
                Name = e.Name,
                Email = e.Email,
                Salary = e.Salary,
                ContactNo = e.ContactNo,
                Address = e.Address,
                RoleName = e.Role.RoleName,
                StatusDescription = e.Status == 1 ? "Active" : "Inactive",
                PolicyName = e.Policy.PolicyName,
                PolicyAmount = e.Policy.PolicyAmount,
                Remaining_Amount = e.Remaining_Amount,
                PolicyDate = e.PolicyDate
            }).Skip(skip).Take(pageSize).ToList();

            int totalRecords = db.EmpRegisters.Count(e => e.Role.RoleName == "Finance Manager" && e.Status == 1);
            int totalPages = totalRecords > 0 ? (int)Math.Ceiling((double)totalRecords / pageSize) : 1;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View("RoleBasedDetails", financeManagers);
        }

        public ActionResult ManagerDetails(int page = 1)
        {
            int pageSize = 5;
            int skip = (page - 1) * pageSize;

            var managers = db.EmpRegisters.Include(e => e.Role).Include(e => e.Policy).Where(e => e.Role.RoleName == "Manager" && e.Status == 1)
            .Select(e => new EmployeeDetailsViewModel
            {
                EmpId = e.EmpId,
                Name = e.Name,
                Email = e.Email,
                Salary = e.Salary,
                ContactNo = e.ContactNo,
                Address = e.Address,
                RoleName = e.Role.RoleName,
                StatusDescription = e.Status == 1 ? "Active" : "Inactive",
                PolicyName = e.Policy.PolicyName,
                PolicyAmount = e.Policy.PolicyAmount,
                Remaining_Amount = e.Remaining_Amount,
                PolicyDate = e.PolicyDate
            })
               .Skip(skip).Take(pageSize).ToList();

            int totalRecords = db.EmpRegisters.Count(e => e.Role.RoleName == "Manager" && e.Status == 1);
            int totalPages = totalRecords > 0 ? (int)Math.Ceiling((double)totalRecords / pageSize) : 1;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View("RoleBasedDetails", managers);
        }

        public ActionResult AccessDenied()
        {
            ViewBag.Message = "You are not authorized to access this page.";
            return View();
        }

        private EmployeeDetailsViewModel GetEmployeeDetails(string employeeId)
        {
            if (!string.IsNullOrEmpty(employeeId))
            {
                return db.EmpRegisters
                    .Include(e => e.Role).Include(e => e.Policy)
                    .Where(e => e.EmpId.ToString() == employeeId)
                    .Select(e => new EmployeeDetailsViewModel
                    {
                        EmpId = e.EmpId,
                        Name = e.Name,
                        Email = e.Email,
                        Salary = e.Salary,
                        Address = e.Address,
                        ContactNo = e.ContactNo,
                        RoleName = e.Role.RoleName,
                        StatusDescription = e.Status == 1 ? "Active" : "Inactive",
                        PolicyName = e.Policy.PolicyName,
                        PolicyAmount = e.Policy.PolicyAmount,
                        Remaining_Amount = e.Remaining_Amount,
                        PolicyDate = e.PolicyDate
                    })
                    .FirstOrDefault();
            }

            return null;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Admin");
        }

        public IActionResult AddPolicy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPolicy(AddPolicyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var policy = new Policy
                {
                    PolicyName = model.PolicyName,
                    PolicyDesc = model.PolicyDesc,
                    PolicyAmount = model.PolicyAmount,
                };

                db.Policies.Add(policy);
                db.SaveChanges();

                return RedirectToAction("ListPolicies"); 
            }

            return View(model);
        }

        public IActionResult ListPolicies()
        {
            var policies = db.Policies.ToList();
            return View(policies);
        }

        public IActionResult Settings()
        {
            var adminId = HttpContext.Session.GetString("AdminId");

            if (string.IsNullOrEmpty(adminId))
            {
                TempData["Error"] = "Invalid session. Please log in again.";
                return RedirectToAction("Login", "Admin");
            }

            var admin = db.Admin.Where(a => a.AdminId.ToString() == adminId).Select(a => new AdminSettingsViewModel
            {
                AdminId = a.AdminId,
                Name = a.Name,
                Email = a.Email,
                Password = a.Password,
                ProfileImage = a.ProfileImage ?? "wwwroot/img" 
            }).FirstOrDefault();

            if (admin == null)
            {
                TempData["Error"] = "Admin not found.";
                return RedirectToAction("Dashboard");
            }

            return View(admin);
        }

        [HttpPost]
        public IActionResult UpdateSettings(AdminSettingsViewModel model, IFormFile profileImage)
        {
            var admin = db.Admin.Find(model.AdminId);

            if (admin != null)
            {
                admin.Name = model.Name;
                admin.Email = model.Email;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    admin.Password = model.Password;
                    // Redirect to the login page after updating the password
                    TempData["SuccessMessage"] = "Password updated successfully! Please log in again.";
                    return RedirectToAction("Login", "Admin");
                }

                if (profileImage != null)
                {
                    var uniqueName = Guid.NewGuid().ToString() + "_" + profileImage.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/profile", uniqueName);

                    // Save the image to the file system
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        profileImage.CopyTo(stream);
                    }

                    // Save the relative image path in the database
                    admin.ProfileImage = "/assets/images/profile/" + uniqueName;  // Relative path to use in the view
                }

                db.SaveChanges();

                TempData["SuccessMessage"] = "Profile updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update profile. Admin not found.";
            }

            return RedirectToAction("Settings", new { id = model.AdminId });
        }

        public IActionResult Profile()
        {
            var adminId = HttpContext.Session.GetString("AdminId");

            if (string.IsNullOrEmpty(adminId))
            {
                TempData["Error"] = "Invalid session. Please log in again.";
                return RedirectToAction("Login", "Admin");
            }

           
            var admin = GetAdminDetails(adminId);

            if (admin == null)
            {
                TempData["Error"] = "Admin not found.";
                return RedirectToAction("Dashboard");
            }

            return View(admin);
        }

        private AdminSettingsViewModel GetAdminDetails(string adminId)
        {
            return db.Admin
                .Where(a => a.AdminId.ToString() == adminId)
                .Select(a => new AdminSettingsViewModel
                {
                    AdminId = a.AdminId,
                    Name = a.Name,
                    Email = a.Email,
                    Password = a.Password,
                    ProfileImage = string.IsNullOrEmpty(a.ProfileImage) ? "/img/default-profile.jpg" : a.ProfileImage
                })
                .FirstOrDefault();
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(string RoleName)
        {
          Role roles = new Role(RoleName);
                    db.Roles.Add(roles);
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Role added successfully!";
                    return RedirectToAction("ListRoles");
        }


        // list Roles
        public IActionResult ListRoles()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }

        public IActionResult EditRole(int id)
        {
            var role = db.Roles.Where(e => e.RoleId == id).Select(e => new Role
            {
                RoleId = e.RoleId,
                RoleName = e.RoleName
            }).FirstOrDefault();

            return View(role);
        }

        [HttpPost]
        public IActionResult EditRole(Role model)
        {
            var role = db.Roles.Find(model.RoleId);

            if (role != null)
            {
                var roles = db.Roles.FirstOrDefault(p => p.RoleId == model.RoleId);

                if (role.RoleId != 1)
                {
                    role.RoleId = model.RoleId;
                    role.RoleName = model.RoleName;
                }
                TempData["SuccessMessage"] = $"Role (<b>{role.RoleName}</b>) was successfully Updated.";
                db.SaveChanges();
               return RedirectToAction("ListRoles");
            }
            TempData["ErrorMessage"] = $"Failed to update details for Role (<b>{role.RoleName}</b>). Please try again.";

            return View(role);
        }

        public IActionResult DeleteRole(int id)
        {
            var role = db.Roles.FirstOrDefault(e => e.RoleId == id);

            if (role != null)
            {
                var Model = new Role
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                };

                return View(Model);
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("DeleteRole")]
        public IActionResult DeleteRoleConfirmed(int id)
        {
            var role = db.Roles.Find(id);

            if (role != null)
            {
                db.Roles.Remove(role);
                TempData["SuccessMessage"] = $"Role (<b>{role.RoleName}</b>) was deleted.";
            }
                db.SaveChanges();
                return RedirectToAction("ListRoles");
        }
    }
}

