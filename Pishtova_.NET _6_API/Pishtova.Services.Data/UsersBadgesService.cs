namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public class UsersBadgesService : IUsersBadgesService
    {
        private readonly PishtovaDbContext db;

        public UsersBadgesService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<UserBadge>> GetByIdAsync(int userBadgeId)
        {
            var operationResult = new OperationResult<UserBadge>();
            if (!operationResult.ValidateNotNull(userBadgeId)) return operationResult;

            try
            {
                var result = await this.db.UsersBadges.Where(x => x.Id == userBadgeId).FirstOrDefaultAsync();
                operationResult.Data = result;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult<int>> CreateAsync(UserBadge userBadge)
        {
            var operationResult = new OperationResult<int>();
            if (!operationResult.ValidateNotNull(userBadge)) return operationResult;

            try
            {
                var saved = await this.db.UsersBadges.AddAsync(userBadge);
                await this.db.SaveChangesAsync();
                operationResult.Data = saved.Entity.Id;
            }
            catch (Exception e)
            {

                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult<ICollection<UserBadge>>> GetAllByTestAsync(int testId)
        {
            var operationResult = new OperationResult<ICollection<UserBadge>>();
            if (!operationResult.ValidateNotNull(testId)) return operationResult;

            try
            {
                var badges = await this.db.UsersBadges
                            .Include(x => x.Badge)
                            .Where(x => x.TestId == testId)
                            .ToListAsync();
                operationResult.Data = badges;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult<ICollection<UserBadge>>> GetAllByUserAsync (string userId)
        {
            var operationResult = new OperationResult<ICollection<UserBadge>>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;

            try
            {
                var badges = await this.db.UsersBadges
                            .Include(x => x.Badge)
                            .Where(x => x.UserId == userId)
                            .ToListAsync();
                operationResult.Data = badges;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

    }
}