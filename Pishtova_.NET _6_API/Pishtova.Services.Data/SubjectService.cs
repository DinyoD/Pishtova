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

    public class SubjectService : ISubjectService
    {
        private readonly PishtovaDbContext db;

        public SubjectService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<Subject>>> GetAllAsync()
        {
            var operationresult = new OperationResult<ICollection<Subject>>();

            try
            {
                var result = await this.db.Subjects.ToListAsync();
                operationresult.Data = result;
            }
            catch (Exception e)
            {
                operationresult.AddException(e);
            }
            return operationresult;
        }

        public async Task<OperationResult<Subject>> GetByIdAsync(int id)
        {
            var operationResult = new OperationResult<Subject>();
            if (!operationResult.ValidateNotNull(id)) return operationResult;

            try
            {
                var result = await this.db.Subjects.FirstOrDefaultAsync(x => x.Id == id);
                operationResult.Data = result;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }
    }
}
