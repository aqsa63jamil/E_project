using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Migrations;

namespace WebApplication2.Controllers
{
    public class Personal_HospitalController : Controller
    {
        Connection ConObj = new Connection();

        public async Task<IActionResult> Index()
        {
            var totalBeds = await ConObj.Per_Hospitals.SumAsync(h => h.TotalBeds);
            var activeBeds = await ConObj.Per_Hospitals.SumAsync(h => h.ActiveBeds);
            var totalDoctors = await ConObj.Per_Hospitals.SumAsync(h => h.TotalDoctors);
            var totalNurses = await ConObj.Per_Hospitals.SumAsync(h => h.TotalNurses);

            ViewBag.TotalBeds = totalBeds;
            ViewBag.ActiveBeds = activeBeds;
            ViewBag.TotalDoctors = totalDoctors;
            ViewBag.TotalNurses = totalNurses;

            // Pass notifications model to the view
            var notifications = await ConObj.Notifications.ToListAsync();
            return View(notifications);
        }



        // Show the form to create a new hospital (Create operation)
        public IActionResult Create()
        {
            return View();
        }

        // Handle the POST request to create a new hospital (Create operation)
        [HttpPost]

        public async Task<IActionResult> Create([Bind("TotalBeds,ActiveBeds,TotalDoctors,TotalNurses")] Per_Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                ConObj.Add(hospital);
                var totalBeds = await ConObj.Per_Hospitals.SumAsync(h => h.TotalBeds);
                var activeBeds = await ConObj.Per_Hospitals.SumAsync(h => h.ActiveBeds);
                var totalDoctors = await ConObj.Per_Hospitals.SumAsync(h => h.TotalDoctors);
                var totalNurses = await ConObj.Per_Hospitals.SumAsync(h => h.TotalNurses);

                ViewBag.TotalBeds = totalBeds;
                ViewBag.ActiveBeds = activeBeds;
                ViewBag.TotalDoctors = totalDoctors;
                ViewBag.TotalNurses = totalNurses;

                // Pass notifications model to the view
                var notifications = await ConObj.Notifications.ToListAsync();
                await ConObj.SaveChangesAsync();
                TempData["Message"] = "Create SuccessFully!";
                return RedirectToAction("Hospital_Index", "Hospital");
            }
            return View(hospital);
        }

        // Show the form to edit a hospital (Update operation)
        public async Task<IActionResult> Edit(int id)
        {
            var hospital = ConObj.Per_Hospitals.Where(e => e.Id == id).Select(e => new Per_Hospital
            {
                Id = e.Id,
                TotalNurses = e.TotalNurses,
                TotalBeds = e.TotalBeds,
                TotalDoctors = e.TotalDoctors,
                ActiveBeds = e.ActiveBeds

            }).FirstOrDefault();
            return View(hospital);
        }

        // Handle the POST request to update a hospital (Update operation)
        [HttpPost]
        public async Task<IActionResult> Edit(Per_Hospital model)
        {
            var hospital = ConObj.EmpRegisters.Find(model.Id);

            if (hospital != null)
            {
                var data = ConObj.Per_Hospitals.Where(h => h.Id == model.Id).FirstOrDefault();

                // Update the properties
                data.TotalBeds = model.TotalBeds;
                data.ActiveBeds = model.ActiveBeds;
                data.TotalDoctors = model.TotalDoctors; // Fixed assignment
                data.TotalNurses = model.TotalNurses;

                // Save changes
                await ConObj.SaveChangesAsync();

                TempData["Message"] = "Edit Successful!";
                return RedirectToAction("Hospital_Index", "Hospital");
            }
            return View();
        }


        [HttpGet]
        // Delete a hospital (Delete operation)
        public async Task<IActionResult>Delete()
        {
            return View("Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await ConObj.Per_Hospitals.FindAsync(id);

            if (hospital == null)
            {
                TempData["Error"] = "Hospital not found!";
                return RedirectToAction("Hospital_Index", "Hospital");
            }

            ConObj.Per_Hospitals.Remove(hospital);
            await ConObj.SaveChangesAsync();

            TempData["Message"] = "Delete Successful!";
            return RedirectToAction("Hospital_Index", "Hospital");
        }


    }
}
