using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using PagedList.Core;
namespace WebApplication2.Controllers
{
    public class HospitalController : Controller
    {
        Connection ConObj = new Connection();

        public async Task SendEmailAsync(string toEmail, string subject, string messageBody)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("wassisheikh448@gmail.com", "tzaccamzaqbpeokt"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("wassisheikh448@gmail.com"),
                Subject = subject,
                Body = messageBody,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);

        }
        [HttpGet]
        public async Task<IActionResult> All_Record(int page = 1, string query = null)
        {
            int pageSize = 5;
            int skip = (page - 1) * pageSize;

            var paymentData = ConObj.Medical_Invoice
                .Where(m => string.IsNullOrEmpty(query) || m.Emp_Name.Contains(query) || m.Emp_Email.Contains(query))
                .Select(m => new Payment_Data
                {
                    TotalAmount = m.TotalAmount,
                    Desc = m.Desc,
                    Emp_Name = m.Emp_Name,
                    Emp_Email = m.Emp_Email
                }).Skip(skip).Take(pageSize).ToList();

            int totalRecords = paymentData.Count(); // Count total records matching the query
            var pagedPaymentData = paymentData.Skip(skip).Take(pageSize).ToList();

            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.AllRecords = paymentData;

            var dashboardViewModel = new DashboardViewModel
            {
                Payment = pagedPaymentData
            };

            if (!string.IsNullOrEmpty(query))
            {
                return PartialView("_PaymentTablePartial", pagedPaymentData);
            }

            return View(dashboardViewModel);
        }


        [HttpGet]
         
        public async Task<IActionResult> Hospital_Index(int page = 1)
        { 
             if (HttpContext.Session.GetString("Email") != null)
            {
            
            
            int pageSize = 5;
            int skip = (page - 1) * pageSize;

            var totalBeds = await ConObj.Per_Hospitals.SumAsync(h => h.TotalBeds);
            var activeBeds = await ConObj.Per_Hospitals.SumAsync(h => h.ActiveBeds);
            var totalDoctors = await ConObj.Per_Hospitals.SumAsync(h => h.TotalDoctors);
            var totalNurses = await ConObj.Per_Hospitals.SumAsync(h => h.TotalNurses);

            var notifications = await ConObj.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .Take(8)
                .ToListAsync();

            var paymentData = ConObj.Medical_Invoice.Select(m => new Payment_Data
            {
                TotalAmount = m.TotalAmount,
                Desc = m.Desc,
                Emp_Name = m.Emp_Name,
                Emp_Email = m.Emp_Email
            }).OrderByDescending(n => n.TotalAmount)
                .Take(5)
                .ToList();

            var totalRecords = await ConObj.Medical_Invoice.CountAsync();

            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var dashboardViewModel = new DashboardViewModel
            {
                TotalBeds = totalBeds,
                ActiveBeds = activeBeds,
                TotalDoctors = totalDoctors,
                TotalNurses = totalNurses,
                Notifications = notifications,
                Payment = paymentData
            };

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(dashboardViewModel);
            }
            else
            {
                return RedirectToAction("LoginPage");
            }
        }

        [HttpGet]
        public IActionResult Visit_Request()
        {
            // Create the model object
            var model = new VisitRequest();
            ViewBag.HospitalId = new SelectList(ConObj.Hospitals.ToList(), "HospitalId", "HospitalName");
            ViewBag.FinanceManagers = new SelectList(ConObj.FinanceManagers.ToList(), "FinanceManagerId", "FullName");
            return View(model);
        }

        [HttpPost]
        public IActionResult Visit_Request(VisitRequest model)
        {
            if (ModelState.IsValid)
            {
                ConObj.VisitRequest.Add(model);

                ConObj.SaveChanges();
                TempData["Message"] = "Employee data Save SuccessFully!";
                return RedirectToAction("Treatment_Request");
            }
            return View(model);
        }


        public IActionResult Treatment_Request()
        {

            ViewBag.Hospitals = new SelectList(ConObj.Hospitals.ToList(), "HospitalId", "HospitalName");
            ViewBag.FinanceManagers = new SelectList(ConObj.FinanceManagers.ToList(), "FinanceManagerId", "FullName");

            return View();
        }

        [HttpPost]

        public IActionResult Treatment_Request(int EmpId, int hospitalId, string treatmentDetails, DateTime requestDate, int FinanceManagerId, string status, DateTime approvalDate, string Emp_Email)
        {

            var treatmentRequest = new TreatmentRequest
            {
                EmpId = EmpId,
                Emp_Email = Emp_Email,
                HospitalId = hospitalId,
                TreatmentDetails = treatmentDetails,
                RequestDate = requestDate,
                ApprovalDate = approvalDate,
                FinanceManagerId = FinanceManagerId,
                Status = status
            };

            ConObj.TreatmentRequests.Add(treatmentRequest);
            ConObj.SaveChanges();
            string approveLink = Url.Action("Approve", "Approved", new { requestId = treatmentRequest.RequestId }, Request.Scheme);
            string rejectLink = Url.Action("Reject", "Approved", new { requestId = treatmentRequest.RequestId }, Request.Scheme);

            string emailBody = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            color: #333;
            line-height: 1.6;
        }}
        h2 {{
            color: #004085;
            font-size: 22px;
        }}
        p {{
            font-size: 14px;
        }}
        .request-details {{
            background-color: #f8f9fa;
            padding: 15px;
            border-radius: 5px;
            margin-top: 10px;
        }}
        .request-item {{
            padding: 8px 0;
            font-weight: bold;
        }}
        .footer {{
            margin-top: 30px;
            font-size: 12px;
            color: #777;
        }}
        .footer a {{
            color: #004085;
            text-decoration: none;
        }}
    </style>
</head>
<body>
    <h2>New Treatment Request</h2>
    <p>We have received a new treatment request from Employee ID: <strong>{EmpId}</strong>.</p>

    <div class='request-details'>
        <h3>Treatment Request Details</h3>
        <p class='request-item'>Treatment Details: <strong>{treatmentDetails}</strong></p>
        <p class='request-item'>Request Date: <strong>{requestDate.ToString("MMMM dd, yyyy")}</strong></p>
        <p class='request-item'>Status: <strong>{status}</strong></p>
    </div>

    <p>To approve or reject this request, please click the respective link below:</p>
    <p>
        <a href='{approveLink}'>Approve</a> | <a href='{rejectLink}'>Reject</a>
    </p>

    <div class='footer'>
        <p>Best regards, <br>
        [Your Hospital Name]<br>
        <a href='mailto:contact@hospital.com'>contact@hospital.com</a><br>
        [Your Hospital Contact Information]<br>
        <a href='http://www.hospitalwebsite.com'>www.hospitalwebsite.com</a></p>
    </div>
</body>
</html>";
            SendEmailAsync("aqsajamil379@gmail.com", "New Treatment Request", emailBody);
            TempData["Message"] = "Treatment Request Sent Successfully!";
            return RedirectToAction("Hospital_Index");
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPage(string Email, string ContactNumber)
        {
            HttpContext.Session.SetString("Email", Email);
            return RedirectToAction("Hospital_Index", "Hospital");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("LoginPage", "Hospital");
        }


    }

}