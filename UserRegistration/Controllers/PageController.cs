using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //checks if the id with page is expired or not
            var isExists = _context.PageExpirations.Any(e => e.token == token); 
            if (isExists) { 
                var pageDetail = _context.PageExpirations.FirstOrDefault(e => e.token == token);
                if (pageDetail != null)
                {
                    if(!pageDetail.IsExpired && pageDetail.ExpirationDate > new DateTimeOffset(DateTime.Now)) {
                        pageDetail.IsOpened = true;
                        pageDetail.IsExpired = true;
                        _context.Entry(pageDetail).State = EntityState.Modified;

                        try
                        {
                            _context.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                                throw;
                        }
                        return View();
                    }
                    else
                    {
                        return View("Submitted");
                    }
                }
            }
            return NotFound();
        }
    }
}
