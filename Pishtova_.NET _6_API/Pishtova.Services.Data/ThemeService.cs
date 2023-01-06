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

    public class ThemeService : IThemeService
    {
        private readonly PishtovaDbContext db;

        public ThemeService(PishtovaDbContext db)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<Theme>>> GetAllBySubjectId(string subjectId)
        {
            var operationResult = new OperationResult<ICollection<Theme>>();
            if (!operationResult.ValidateNotNull(subjectId)) return operationResult;

            try
            {
                var themes = await this.db.Themes.Where(x => x.SubjectId == subjectId).ToListAsync();
                return operationResult.WithData(themes);
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
                return operationResult;
            }
        }
    }
}
