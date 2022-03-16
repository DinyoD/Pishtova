namespace Pishtova.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.UserBadge;
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


        public async Task CreateUserBadgeAsync(UserBadgeModel model)
        {
            await this.db.UsersBadges.AddAsync(new UserBadge { UserId = model.UserId, BadgeId = model.BadgeId, TestId = model.TestId });
        }

        public async Task<ICollection<UserBadge>> GetUserAllBadgesAsync (string userId)
        {
            return await this.db.UsersBadges.Include(x=>x.Badge).Where(x => x.UserId == userId).ToListAsync();
        } 
    }
}