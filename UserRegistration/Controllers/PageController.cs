using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System;
using System.Linq;
using UserRegistration.DAL;
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

        public async Task<IActionResult> IndexAsync(string token)
        {
            ViewData["Token"] = token;

            //getPageDetailByToken
            var pageDetail = await _pageExpiration.GetPageExpirationByToken(token);
            /*            _context.PageExpirations.FirstOrDefault(e => e.token == token);*/
            if (pageDetail != null && !pageDetail.IsExpired && pageDetail.ExpirationDate > DateTimeOffset.Now)
            {
                if (!pageDetail.IsOpened)
                {//
                 //UpdatePageDetail

                    await _pageExpiration.UpdateOpened(pageDetail);
                }

                return View();
            }
            return View("Submitted");
        }



        [HttpPost]
        public async Task<IActionResult> SubmitAsync(Patient patient)
        {
            if (ModelState.IsValid)
            {
                /*_context.Patients.Add(patient);*/
                var pageDetail = await _pageExpiration.GetPageExpirationByToken(patient.PageGuid);
/*                var pageExpiration = _context.PageExpirations.FirstOrDefault(e => e.token == patient.PageGuid);*/
                if (pageDetail != null)
                {
                    await _pageExpiration.UpdateSubmitted(pageDetail);
                    bool result = Task.Run(async () => await _saveData.Save(patient)).Result;

                }

                return RedirectToAction("Index", new { token = patient.PageGuid });
            }

            ViewData["Token"] = patient.PageGuid;
            return View("Index", patient);
        }
    }
}
