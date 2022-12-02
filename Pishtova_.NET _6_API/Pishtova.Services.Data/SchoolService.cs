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

    public class SchoolService : ISchoolService
    {
        private readonly PishtovaDbContext db;

        public SchoolService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<School>>> GetAllByTownIdAsync(int townId)
        {
            var operationResult = new OperationResult<ICollection<School>>();
            if (!operationResult.ValidateNotNull(townId)) return operationResult;

            try
            {
                var schools =  await this.db.Schools
                    .Where(x => x.TownId == townId)
                    .ToListAsync();
                operationResult.Data = schools;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }
    }
}
