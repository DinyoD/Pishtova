namespace Pishtova.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersBadgesService : IUsersBadgesService
    {
        private readonly PishtovaDbContext db;

        public UsersBadgesService(PishtovaDbContext db)
        {
            this.db = db;
        }


        public async Task CreateUserBadgeAsync(string userId, string badgeId)
        {
            await this.db.UsersBadges.AddAsync(new UserBadge { UserId = userId, BadgeId = badgeId });
        }

        public async Task<ICollection<UserBadge>> GetUserAllBadgesAsync (string userId)
        {
            return await this.db.UsersBadges.Include(x=>x.Badge).Where(x => x.UserId == userId).ToListAsync();
        } 
    }
}