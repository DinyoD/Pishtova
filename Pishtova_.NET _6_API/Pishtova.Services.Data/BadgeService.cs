namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Pishtova.Data;
    using Pishtova.Data.Common.Utilities;
    using Pishtova.Data.Model;

    public class BadgeService : IBadgeService
    {
        private readonly PishtovaDbContext db;

        public BadgeService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<Badge>> GetByCodeAsync(int badgeCode)
        {
            var operationResult = new OperationResult<Badge>();
            if (!operationResult.ValidateNotNull(badgeCode)) return operationResult;

            try
            {
                var badge = await this.db.Badges.Where(x => x.Code == badgeCode).FirstOrDefaultAsync();
                operationResult.Data = badge;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }

            return operationResult;
        }
    }
}
