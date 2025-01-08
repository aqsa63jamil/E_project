using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using System.Linq;
using WebApplication2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Data;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        Connection empdb = new Connection();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult FeedBack()
        {
            return View();
        }
        public IActionResult EmpLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmpLogin(EmployeeLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = empdb.EmpRegisters.Include(e => e.Role).FirstOrDefault(e => e.Email == model.Email);

                if (employee != null)
                {
                    var passwordHasher = new PasswordHasher<Employee>();
                    var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, employee.Password, model.Password);

                    if (passwordVerificationResult == PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("EmployeeId", employee.EmpId.ToString());
                        HttpContext.Session.SetString("EmployeeRole", employee.Role.RoleName);

                        if (!string.IsNullOrEmpty(employee.ProfilePicture))
                        {
                            HttpContext.Session.SetString("ProfileImage", employee.ProfilePicture);
                        }

                        if (employee.Role.RoleName == "Employee")
                        {
                            if (employee.Status == 1)
                            {
                                return RedirectToAction("EmployeeProfile", "Home");
                            }
                            else
                            {
                                return RedirectToAction("NotActive");
                            }

                        }
                        else if (employee.Role.RoleName == "Finance Manager")
                        {
                            return RedirectToAction("FinanceManagerProfile");
                        }
                        else if (employee.Role.RoleName == "Manager")
                        {
                            return RedirectToAction("ManagerProfile");
                        }
                    }
                }

                ViewBag.Error = "Invalid email or password.";
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("EmpLogin", "Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            var employeeId = HttpContext.Session.GetString("EmployeeId");

            if (string.IsNullOrEmpty(employeeId))
            {
                return RedirectToAction("EmpLogin", "Home");
            }

            var employee = empdb.EmpRegisters.Find(Convert.ToInt32(employeeId));

            if (employee == null)
            {
                return NotFound();
            }

            var passwordHasher = new PasswordHasher<Employee>();

            var passwordVerificationresult = passwordHasher.VerifyHashedPassword(null, employee.Password, CurrentPassword);

            if (passwordVerificationresult != PasswordVerificationResult.Success)
            {
                TempData["ErrorMessage"] = "Current Password is Incorrect.";
                //ViewBag.Error = "Current Password is Incorrect.";
                return View();
            }

            if (NewPassword != ConfirmPassword)
            {
                TempData["ErrorMessage"] = "New Password and confirm Password do no match.";
                //ViewBag.Error = "New Password and confirm Password do no match.";
                return View();
            }

            employee.Password = passwordHasher.HashPassword(null, NewPassword);

            empdb.SaveChanges();

            TempData["SuccessMessage"] = "Password changed Successfully.";

            return RedirectToAction("EmpLogin", "Home");
        }

        public ActionResult EmployeeProfile()
        {
            var employeeId = HttpContext.Session.GetString("EmployeeId");
            var employeeRole = HttpContext.Session.GetString("EmployeeRole");
           
                if (employeeRole != "Employee")
            {
                return RedirectToAction("AccessDenied", "Admin");
            }

            var employee = GetEmployeeDetails(employeeId);
            if (HttpContext.Session.GetString("EmployeeId") != null)
            {
                var randomStartDate = HttpContext.Session.GetString("RandomStartDate");
                if (string.IsNullOrEmpty(randomStartDate))
                {
                    randomStartDate = DateTime.Now.AddDays(-new Random().Next(0, 30)).ToString("yyyy-MM-dd");
                    HttpContext.Session.SetString("RandomStartDate", randomStartDate);
                }

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("ProfileImage")) && !string.IsNullOrEmpty(employee.ProfilePicture))
                {
                    HttpContext.Session.SetString("ProfileImage", employee.ProfilePicture);
                }

              
                ViewBag.ProfileImage = HttpContext.Session.GetString("ProfileImage");

                ViewBag.StartDate = randomStartDate;
                return View(employee);
            }

            return RedirectToAction("EmpLogin");
        }

        [HttpPost]
        public IActionResult UploadProfilePicture(IFormFile file)
        {
            var employeeIdString = HttpContext.Session.GetString("EmployeeId");
            if (string.IsNullOrEmpty(employeeIdString) || !int.TryParse(employeeIdString, out int employeeId))
            {
                return RedirectToAction("EmpLogin", "Home");
            }

            var employee = empdb.EmpRegisters.FirstOrDefault(e => e.EmpId == employeeId);
            if (employee == null)
            {
                return RedirectToAction("EmpLogin", "Home");
            }

                    var uniqueName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", uniqueName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    employee.ProfilePicture = "/img/" + uniqueName;

                    empdb.SaveChanges();

                    HttpContext.Session.SetString("ProfileImage", employee.ProfilePicture);
           

            return RedirectToAction("EmployeeProfile");
        }




        private EmployeeDetailsViewModel GetEmployeeDetails(string employeeId)
        {
            if (!string.IsNullOrEmpty(employeeId))
            {
                var randomStartDate = DateTime.Now.AddDays(-new Random().Next(0, 30));

                return empdb.EmpRegisters.Include(e => e.Role).Include(e => e.Policy).Where(e => e.EmpId.ToString() == employeeId).Select(e => new EmployeeDetailsViewModel
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
                    RandomStartDate = randomStartDate
                }).FirstOrDefault();
            }

            return null;
        }

        public IActionResult Profile()
        {
            var employeeId = HttpContext.Session.GetString("EmployeeId");

            if (string.IsNullOrEmpty(employeeId))
            {
                return RedirectToAction("EmpLogin", "Home");
            }

            var employee = empdb.EmpRegisters.Where(e => e.EmpId.ToString() == employeeId).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Update(int id)
        {
            var employee = empdb.EmpRegisters.Where(e => e.EmpId == id).Select(e => new EmployeeDetailsViewModel
            {
                EmpId = e.EmpId,
                Name = e.Name,
                Email = e.Email,
                Address = e.Address,
                ContactNo = e.ContactNo,
            }).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Update(EmployeeDetailsViewModel model)
        {
           
                var employee = empdb.EmpRegisters.Find(model.EmpId);

                if (employee == null)
                {
                    return NotFound();
                }
            else
            {
                    employee.Name = model.Name;
                    employee.Email = model.Email;
                    employee.Address = model.Address;
                    employee.ContactNo = model.ContactNo;

            }
                    empdb.SaveChanges();

                    return RedirectToAction("EmployeeProfile");
            
        }
    }
}
