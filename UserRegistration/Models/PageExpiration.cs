namespace UserRegistration.Models
{
    public class PageExpiration
    {
        public Guid Id { get; set; }
        public string token { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public bool IsExpired { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsOpened { get; set; }
        public string BitlyUrl { get; set; }
    }
}
