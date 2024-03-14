using Microsoft.EntityFrameworkCore;
using UserRegistration.Models;

namespace UserRegistration.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PageExpiration> PageExpirations { get; set; }

    }
}
