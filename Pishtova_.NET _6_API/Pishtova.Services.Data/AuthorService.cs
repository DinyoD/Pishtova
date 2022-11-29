namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Pishtova.Data;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Model.Author;
    using Pishtova_ASP.NET_web_api.Model.Work;

    public class AuthorService : IAuthorService
    {
        private readonly PishtovaDbContext db;

        public AuthorService(PishtovaDbContext db)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<AuthorModel>>> GetAuthorsWithWorksAsync(int subjectId)
        {
            var operationResult = new OperationResult<ICollection<AuthorModel>>();
            if (!operationResult.ValidateNotNull(subjectId)) return operationResult;

            try
            {
                var authorModels = await this.db.Authors
                .Include(x => x.Works)
                .Select(x => new AuthorModel
                {
                    Name = x.Name,
                    Index = x.Index,
                    Works = x.Works
                        .Where(w => w.SubjectId == subjectId)
                        .Select(w => new WorkModel
                        {
                            Name = w.Name,
                            Index = w.Index
                        })
                        .ToList()
                })
                .Where(x => x.Works.Count > 0)
                .ToListAsync();
                operationResult.Data = authorModels;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }
    }
}
