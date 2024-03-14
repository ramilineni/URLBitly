using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserRegistration.Data;
using UserRegistration.Models;



namespace UserRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageExpirationsController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly ApplicationDbContext _context;

        public PageExpirationsController(ApplicationDbContext context)
        {
            _context = context;
        }

  

        [HttpPost]
        public async Task<ActionResult<PageExpiration>> PostPageExpiration()
        {
            string randomString = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());

            var pageExpiration = new PageExpiration()
            {
                Id = Guid.NewGuid(),
                ExpirationDate = DateTimeOffset.Now.AddDays(2),
                CreatedDate = DateTimeOffset.Now,
                IsExpired = false,
                IsOpened = false,
                token = randomString
            };

            var host = HttpContext.Request.Host.Value;
            var longUrl = $"https://{host}/Page/Index?token={randomString}";


            pageExpiration.BitlyUrl = longUrl;

            _context.PageExpirations.Add(pageExpiration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPageExpiration", new { id = pageExpiration.Id }, pageExpiration);
        }
      
    }
}
