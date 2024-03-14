using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Models
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set;}
        public string PageGuid { get; set; }
    }
}
