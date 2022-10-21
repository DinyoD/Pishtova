namespace Pishtova.Services.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Pishtova.Data;
    using Pishtova.Data.Model;

    public class UsersBadgesService : IUsersBadgesService
    {
        private readonly PishtovaDbContext db;

        public UsersBadgesService(PishtovaDbContext db)
        {
            this.db = db;
        }


        public async Task CreatAsync(UserBadge userBadge)
        {
            await this.db.UsersBadges.AddAsync(userBadge);
            await this.db.SaveChangesAsync();
        }

        public async Task<ICollection<UserBadge>> GetAllByTestAsync(int testId)
        {
            return await this.db.UsersBadges
                .Include(x => x.Badge)
                .Where(x => x.TestId == testId)
                .ToListAsync();
        }

        public async Task<ICollection<UserBadge>> GetAllByUserAsync (string userId)
        {
            return await this.db.UsersBadges
                .Include(x=>x.Badge)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        } 
    }
}