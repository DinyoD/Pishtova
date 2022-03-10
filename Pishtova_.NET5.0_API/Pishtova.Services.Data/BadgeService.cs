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
        public async Task<Badge> GetBadgeByCodeAsync(int badgeCode)
        {
            return await this.db.Badges.Where(x => x.Code == badgeCode).FirstOrDefaultAsync();
        }
    }
}
