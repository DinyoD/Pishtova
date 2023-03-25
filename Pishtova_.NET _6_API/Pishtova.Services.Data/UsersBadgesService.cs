namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

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
                var result = await this.db.UsersBadges.FirstOrDefaultAsync(x => x.Id == userBadgeId);
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
                            .Where(x => x.TestId == testId)
                            .Include(x => x.Badge)
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
                            .Where(x => x.UserId == userId)
                            .Include(x => x.Badge)
                            .ToListAsync();
                operationResult.Data = badges;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult<int>> DeleteByUserIdAsync(string userId)
        {

            var operationResult = new OperationResult<int>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;

            try
            {
                var userBadgesToRemove = await this.db.UsersBadges.Where(x => x.UserId == userId).ToListAsync();
                if (userBadgesToRemove.Count == 0) return operationResult.WithData(0);

                this.db.UsersBadges.RemoveRange(userBadgesToRemove);
                await this.db.SaveChangesAsync();
                operationResult.Data = userBadgesToRemove.Count;

            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

    }
}