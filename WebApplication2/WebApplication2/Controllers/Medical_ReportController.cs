
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Mail;
using iText.Layout;
using MailKit.Net.Smtp;

using WebApplication2.Models;
using Org.BouncyCastle.Ocsp;

namespace WebApplication2.Controllers
{
    public class Medical_ReportController : Controller
    {
        Connection ConObj = new Connection();
        public IActionResult CreateMedicalInvoice()
        {
            ViewBag.PolicyList = new SelectList(ConObj.Policies, "PolicyId", "PolicyName");
            ViewBag.RoleList = new SelectList(ConObj.Roles, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateMedicalInvoice(int EmpId, int roleId, decimal TotalAmount, string desc, string Emp_Name, string Emp_Email, DateTime CreateDate)
        {
            Medical_Invoice MedicalObj = new Medical_Invoice
            {
                EmpId = EmpId,
                RoleId = roleId,
                TotalAmount = TotalAmount,
                Desc = desc,
                Emp_Name = Emp_Name,
                CreateDate = CreateDate,
                Emp_Email = Emp_Email
            };

            if (ModelState.IsValid)
            {
                // Save Medical Invoice to the database
                ConObj.Medical_Invoice.Add(MedicalObj);
                ConObj.SaveChanges();

                // Retrieve related entities
                var employee = ConObj.EmpRegisters.FirstOrDefault(e => e.EmpId == MedicalObj.EmpId);
                var admin = ConObj.Admin.FirstOrDefault();
                var manager = ConObj.FinanceManagers.FirstOrDefault();

                // Check if emails exist
                if (!string.IsNullOrEmpty(employee?.Email) &&
                    !string.IsNullOrEmpty(admin?.Email) &&
                    !string.IsNullOrEmpty(manager?.Email))
                {
                    // Generate email bodies
                    string adminBody = GenerateEmailBody(MedicalObj, false); // No button for Admin
                    string employeeBody = GenerateEmailBody(MedicalObj, false); // No button for Employee
                    string managerBody = GenerateEmailBody(MedicalObj, true);  // Button for Finance Manager

                    string pdfFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MedicalInvoice.pdf");
                    CreatePdfWithInvoice(pdfFilePath, MedicalObj);
                    // Send emails to Employee, Admin, and Finance Manager
                    SendEmailAsync(employee.Email, "New Medical Invoice Notification", employeeBody, pdfFilePath);
                    SendEmailAsync(admin.Email, "New Medical Invoice Notification", adminBody, pdfFilePath);
                    SendEmailAsync(manager.Email, "New Medical Invoice Notification", managerBody, pdfFilePath);

                    TempData["Message"] = "Emails sent to Employee, Admin, and Finance Manager successfully!";
                    return RedirectToAction("Hospital_Index", "Hospital");
                }
                else
                {
                    ModelState.AddModelError("", "Email addresses are missing for one or more recipients.");
                }
            }
            return View(MedicalObj);
        }
        private string GetEmailCssStyles()
        {
            return @"
    <style>
        body {
        font-family: Arial, sans-serif;
        color: #4a4a4a;
        line-height: 1.8;
        margin: 0;
        padding: 0;
        background-color: #f9f9f9;
    }

    table {
        width: 100%;
        border-collapse: collapse;
        margin: 20px auto;
        max-width: 600px;
        background-color: #ffffff;
        border: 1px solid #ddd;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
    }

    td {
        padding: 15px;
        font-size: 16px;
        color: #4a4a4a;
    }

    h2 {
        color: #007bff;
        font-size: 24px;
        text-align: center;
        margin-bottom: 10px;
    }

    p {
        font-size: 16px;
        margin-bottom: 10px;
    }

    .button {
        display: block;
        width: fit-content;
        padding: 12px 20px;
        margin: 20px auto;
        font-size: 14px;
        font-weight: bold;
        color: #ffffff;
        background-color: #007bff;
        text-decoration: none;
        border-radius: 5px;
        text-align: center;
    }

    .invoice-table {
        width: 100%;
        border-collapse: collapse;
        margin: 20px 0;
    }

    .invoice-table td {
        padding: 12px;
        border: 1px solid #ddd;
        text-align: left;
    }

    .invoice-table th {
        padding: 12px;
        background-color: #007bff;
        color: #ffffff;
        border: 1px solid #ddd;
        text-align: left;
    }

    .footer {
        margin-top: 20px;
        text-align: center;
    }

    .footer a {
        color: #007bff;
        text-decoration: none;
    }
    </style>";
        }
        private string GenerateEmailBody(Medical_Invoice MedicalObj, bool isForFinanceManager)
        {
            // Get shared CSS styles
            string cssStyles = GetEmailCssStyles();

            // Add button only for the Finance Manager
            string buttonHtml = string.Empty;
            if (isForFinanceManager)
            {
                buttonHtml = $@"
            <a href='https://www.yourhospital.com/approve-invoice/{MedicalObj.MedicalId}' class='button'>Review and Approve</a>";
            }

            // Email content
            return $@"
    <html>
    <head>
        {cssStyles}
    </head>
    <body>
        <table>
        <tr>
            <td style='padding: 20px; background-color: #f4f4f4; text-align: center;'>
                <h2>New Medical Invoice Created</h2>
            </td>
        </tr>
        <tr>
            <td style='padding: 20px;'>
                <p>Dear {MedicalObj.Emp_Name},</p>
                <p>We are writing to inform you that a new medical invoice has been created for you. Please review the details below:</p>
                
                <table class='invoice-table'>
                    <tr>
                        <td><strong>Invoice ID:</strong> {MedicalObj.MedicalId}</td>
                        <td><strong>Employee Name:</strong> {MedicalObj.Emp_Name}</td>
                    </tr>
                    <tr>
                        <td><strong>Total Amount:</strong> {MedicalObj.TotalAmount:C}</td>
                        <td><strong>Description:</strong> {MedicalObj.Desc}</td>
                    </tr>
                </table>

                {buttonHtml}

                <p>Please take the necessary action based on the invoice details.</p>
                <p>If you have any questions or need further assistance, feel free to contact us at <a href='mailto:support@yourhospital.com'>support@yourhospital.com</a>.</p>
                
                <p>Thank you for your attention to this matter.</p>

                <div class='footer'>
                    <p>Best regards,<br>
                    <strong>Your Hospital Name</strong><br>
                    <a href='http://www.yourhospital.com'>www.yourhospital.com</a><br>
                    <a href='mailto:support@yourhospital.com'>support@yourhospital.com</a></p>
                </div>
            </td>
        </tr>
    </table>
    </body>
    </html>";
        }

        public void CreatePdfWithInvoice(string pdfFilePath, Medical_Invoice medicalObj)
        {
            // Create a PdfWriter object to write to the file
            using (var writer = new PdfWriter(pdfFilePath))
            {
                // Create a PdfDocument object using the PdfWriter
                using (var pdf = new PdfDocument(writer))
                {
                    // Create a Document object to add content
                    var document = new Document(pdf);

                    // Add the invoice title
                    document.Add(new Paragraph("Medical Invoice")
                        .SetFontSize(18).SetBold());

                    // Add invoice details
                    document.Add(new Paragraph($"Invoice ID: {medicalObj.MedicalId}"));
                    document.Add(new Paragraph($"Employee Name: {medicalObj.Emp_Name}"));
                    document.Add(new Paragraph($"Total Amount: {medicalObj.TotalAmount:C}"));
                    document.Add(new Paragraph($"Description: {medicalObj.Desc}"));
                    document.Add(new Paragraph($"Date: {DateTime.Now.ToString("MM/dd/yyyy")}"));

                    // Close the PDF document
                    document.Close();
                }
            }
        }



        public async Task SendEmailAsync(string toEmail, string subject, string messageBody, string pdfFilePath)
        {
            using (var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("wassisheikh448@gmail.com", "jejieaaegtrvhuhm"),
                EnableSsl = true,
            })
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("wassisheikh448@gmail.com"),
                    Subject = subject,
                    Body = messageBody,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);





                var attachment = new Attachment(pdfFilePath);
                mailMessage.Attachments.Add(attachment);


                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}