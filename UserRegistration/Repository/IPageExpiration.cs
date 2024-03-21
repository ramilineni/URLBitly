using UserRegistration.Models;

namespace UserRegistration.Repository
{
    public interface IPageExpiration
    {
        public  Task<PageExpiration> GetPageExpirationByToken(string token);
        public  Task<bool> UpdateOpened(PageExpiration pageExpiration);
        public Task<bool> UpdateSubmitted(PageExpiration pageExpiration);
    }
}