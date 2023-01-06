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

    public class PoemService : IPoemService
    {
        private readonly PishtovaDbContext db;

        public PoemService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<Poem>>> GetAllByThemeId(string themeId)
        {
            var operationResult = new OperationResult<ICollection<Poem>>();
            if (!operationResult.ValidateNotNull(themeId)) return operationResult;

            try
            {
                var poems = await this.db.Poems.Where(x => x.ThemeId == themeId).Include(x => x.Author).ToListAsync();
                return operationResult.WithData(poems);
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
                return operationResult;
            }
        }

        public async Task<OperationResult<Poem>> GetById(string id)
        {
            var operationResult = new OperationResult<Poem>();
            if (!operationResult.ValidateNotNull(id)) return operationResult;

            try
            {
                var poem = await this.db.Poems.FirstOrDefaultAsync(x => x.Id == id);
                return operationResult.WithData(poem);
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
                return operationResult;
            }
        }
    }
}
