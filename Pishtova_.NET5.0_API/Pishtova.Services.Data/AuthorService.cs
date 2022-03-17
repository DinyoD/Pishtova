namespace Pishtova.Services.Data
{
    using Pishtova.Data;
    using Pishtova_ASP.NET_web_api.Model.Author;
    using Pishtova_ASP.NET_web_api.Model.Work;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly PishtovaDbContext db;

        public AuthorService(PishtovaDbContext db)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        public List<AuthorDTO> GetAuthorsWithWorks(int subjectId)
        {
            return this.db.Authors
                .Where(x => x.Works.Any(w => w.SubjectId == subjectId))
                .Select(x => new AuthorDTO
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
                .ToList();
        }
    }
}
