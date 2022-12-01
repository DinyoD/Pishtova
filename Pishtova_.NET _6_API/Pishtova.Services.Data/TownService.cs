namespace Pishtova.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Pishtova.Data;
    using Pishtova.Data.Common.Utilities;
    using Pishtova.Data.Model;

    public class TownService : ITownService
    {
        private readonly PishtovaDbContext db;

        public TownService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<Town>>> GetAllByMunicipalityId(int municipalityId)
        {
            var operationResult = new OperationResult<ICollection<Town>>();
            if (!operationResult.ValidateNotNull(municipalityId)) return operationResult;

            try
            {
                var towns = await this.db.Towns
                                            .Where(x => x.MunicipalityId == municipalityId)
                                            .ToListAsync();
                operationResult.Data = towns;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }

            return operationResult;
        }
    }
}
