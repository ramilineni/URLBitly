using UserRegistration.DAL;
using UserRegistration.Interface;
using UserRegistration.Models;

namespace UserRegistration.Repository
{
    public class PageExpirationRepository : IPageExpiration
    {
        private IAmeliaDBDAL _ameliaDb;
        public PageExpirationRepository(IAmeliaDBDAL ameliaDb) {
            _ameliaDb = ameliaDb;
        }

        public async Task<PageExpiration> GetPageExpirationByToken(string token)
        {
            var pageExpiration =  await _ameliaDb.GetPageExpirationByToken(token);
            return pageExpiration[0];
        }

        public async Task<bool> UpdateOpened(PageExpiration pageExpiration)
        {
            pageExpiration.IsOpened = true;
            await _ameliaDb.UpdatePageExpiration(pageExpiration, false);
            return true;
        }

        public async Task<bool> UpdateSubmitted(PageExpiration pageExpiration)
        {
            pageExpiration.IsExpired = true;
            pageExpiration.SubmissionDate = DateTimeOffset.Now;
            await _ameliaDb.UpdatePageExpiration(pageExpiration, true);
            return true;
        }
    }
}
