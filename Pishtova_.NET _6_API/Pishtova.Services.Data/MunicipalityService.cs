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

    public class MunicipalityService : IMunicipalityService
    {
        private readonly PishtovaDbContext db;

        public MunicipalityService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<Municipality>>> GetAllAsync()
        {
            var operationResult = new OperationResult<ICollection<Municipality>>();

            try
            {
                var municipality = await  this.db.Municipalities
                    .ToListAsync();

                operationResult.Data = municipality;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }

            return operationResult;
        }

        public async Task<OperationResult<Municipality>> GetByIdAsync(int id)
        {
            var operationResult = new OperationResult<Municipality>();
            if (!operationResult.ValidateNotNull(id)) return operationResult;

            try
            {
                var municipality =  await this.db.Municipalities
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                operationResult.Data = municipality;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }

            return operationResult;
        }
    }
}
