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

    public class AuthorService : IAuthorService
    {
        private readonly PishtovaDbContext db;

        public AuthorService(PishtovaDbContext db)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<Author>>> GetAuthorsWithWorksBySubjectIdAsync(int subjectId)
        {
            var operationResult = new OperationResult<ICollection<Author>>();
            if (!operationResult.ValidateNotNull(subjectId)) return operationResult;

            try
            {
                var authors = await this.db.Authors
                    .Where(x => x.Works.Any(x => x.SubjectId == subjectId))
                    .Include(x => x.Works)
                    .ToListAsync();
                foreach (var author in authors)
                {
                    author.Works = author.Works.Where(x => x.SubjectId == subjectId).ToList();
                }
                operationResult.Data = authors;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }
    }
}
