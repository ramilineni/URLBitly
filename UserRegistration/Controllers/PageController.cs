using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UserRegistration.Data;
using UserRegistration.Interface;
using UserRegistration.Models;
using UserRegistration.Models.RequestModels;
using UserRegistration.Repository;

namespace UserRegistration.Controllers
{
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICallToSaveData _saveData;
        private readonly IPageExpiration _pageExpiration;

        public PageController(ApplicationDbContext context, ICallToSaveData saveData, IPageExpiration pageExpiration)
        {
            _context = context;
            _saveData = saveData;
            _pageExpiration = pageExpiration;
        }

        public IActionResult Index(string token)
        {
            ViewData["Token"] = token;

            //getPageDetailByToken
            var pageDetail =  _pageExpiration.GetPageExpirationByToken(token);
             _context.PageExpirations.FirstOrDefault(e => e.token == token);
            if (pageDetail != null && !pageDetail.IsExpired && pageDetail.ExpirationDate > DateTimeOffset.Now)
            {
                if (!pageDetail.IsOpened)
                {//
                    //UpdatePageDetail
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
                    pageExpiration.SubmissionDate = DateTimeOffset.Now;
                    _context.Entry(pageExpiration).State = EntityState.Modified;
                    bool result = Task.Run(async () => await _saveData.Save(patient)).Result;

                }
               
                _context.SaveChanges();

                return RedirectToAction("Index", new { token = patient.PageGuid });
            }

            ViewData["Token"] = patient.PageGuid;
            return View("Index", patient);
        }
    }
}
