namespace Pishtova.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using System.Linq;
    using System.Threading.Tasks;

    public class BadgeService : IBadgeService
    {
        private readonly PishtovaDbContext db;

        public BadgeService(PishtovaDbContext db)
        {
            this.db = db;
        }
        public async Task<string> GetBadgeIdByCodeAsync(int badgeCode)
        {
            var badge = await this.db.Badges.Where(x => x.Code == badgeCode).FirstOrDefaultAsync();
            return badge.Id;
        }
    }
}
