using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UserRegistration.Data;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string token)
        {
            ViewData["Token"] = token;

            var pageDetail = _context.PageExpirations.FirstOrDefault(e => e.token == token);
            if (pageDetail != null && !pageDetail.IsExpired && pageDetail.ExpirationDate > DateTimeOffset.Now)
            {
                if (!pageDetail.IsOpened)
                {
                    pageDetail.IsOpened = true;
                    _context.Entry(pageDetail).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                return View();
            }
            return View("Submitted");
        }

        [HttpPost]
        public IActionResult Submit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                var pageExpiration = _context.PageExpirations.FirstOrDefault(e => e.token == patient.PageGuid);
                if (pageExpiration != null)
                {
                    pageExpiration.IsExpired = true;
                    _context.Entry(pageExpiration).State = EntityState.Modified;
                }

                _context.SaveChanges();

                return RedirectToAction("Index", new { token = patient.PageGuid });
            }

            ViewData["Token"] = patient.PageGuid;
            return View("Index", patient);
        }
    }
}
