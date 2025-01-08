using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace WebApplication2.Controllers
{
    public class ApprovedController : Controller
    {
        Connection ConObj = new Connection();

        public async Task SendEmailAsync(string toEmail, string subject, string messageBody)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("aqsajamil379@gmail.com", "lcljeblcsrlfxyoi"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("aqsajamil379@gmail.com"),
                Subject = subject,
                Body = messageBody,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);

        }
        public IActionResult Approve(int requestId)
        {
            Console.WriteLine($"RequestId: {requestId}");

            var treatmentRequest = ConObj.TreatmentRequests.Find(requestId);

            if (treatmentRequest == null)
            {
                return NotFound("No treatment request found with the given ID.");


            }
            return View(treatmentRequest);
        }

            [HttpPost]
        public async Task<IActionResult> Approve(int requestId, string status)
        {
            var treatmentRequest = await ConObj.TreatmentRequests.FindAsync(requestId);

            if (treatmentRequest == null)
            {
                return NotFound();
            }

            treatmentRequest.Status = status;
            treatmentRequest.ApprovalDate = DateTime.Now;
            var notification = new Notification
            {
                TreatmentRequestId = treatmentRequest.RequestId,
                Message = $"The treatment request for Employee {treatmentRequest.EmpId} has been {status}.",
                CreatedAt = DateTime.Now
            };
            ConObj.Notifications.Add(notification);
          
            await ConObj.SaveChangesAsync();
     
            string subject = $"Treatment Request {status}";
            string body = $"The treatment request for Employee {treatmentRequest.EmpId} has been {status}.";
            await SendEmailAsync("wassisheikh448@gmail.com", subject, body);
            TempData["Message"] = "Approved Email send SuccessFully!";
            return RedirectToAction("Hospital_Index","Hospital");
        }


        public IActionResult Reject(int requestId)
        {
            Console.WriteLine($"RequestId: {requestId}");

            var treatmentRequest = ConObj.TreatmentRequests.Find(requestId);

            if (treatmentRequest == null)
            {
                Console.WriteLine("Treatment request not found.");
                return NotFound("No treatment request found with the given ID.");
            }
            return View(treatmentRequest);
        }



        // Reject POST
        [HttpPost]
        public async Task<IActionResult> Reject(int requestId, string status)
        {
            var treatmentRequest = await ConObj.TreatmentRequests.FindAsync(requestId);

            if (treatmentRequest == null)
            {
                return NotFound();
            }

            if (status == "Pending")
            {
                treatmentRequest.Status = status;
                treatmentRequest.ApprovalDate = DateTime.Now;
            }

            // Notification add karna
            var notification = new Notification
            {
                TreatmentRequestId = treatmentRequest.RequestId,
                Message = $"The treatment request for Employee {treatmentRequest.EmpId} has been {status}.",
                CreatedAt = DateTime.Now
            };
            ConObj.Notifications.Add(notification);
      
            // Save changes database me
            await ConObj.SaveChangesAsync();
            string subject = $"Treatment Request {status}";
            string body = $"The treatment request for Employee {treatmentRequest.EmpId} has been {status}.";
            await SendEmailAsync("wassisheikh448@gmail.com", subject, body);
            TempData["Message"] = "Rejected Email Send SuccessFully!";
            return RedirectToAction("Hospital_Index","Hospital");
        }
    }
}
