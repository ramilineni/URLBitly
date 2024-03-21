using UserRegistration.Models;

namespace UserRegistration.DAL
{
    public interface IAmeliaDBDAL
    {
        public Task<List<PageExpiration>> GetPageExpirationByToken(string token);
        public Task<bool> UpdatePageExpiration(PageExpiration pageExpiration, bool isSubmitted);

    }
}