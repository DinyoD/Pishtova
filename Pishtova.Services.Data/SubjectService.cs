namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pishtova.Data;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using Microsoft.EntityFrameworkCore;

    public class SubjectService : ISubjectService
    {
        private readonly PishtovaDbContext db;

        public SubjectService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ICollection<SubjectModel>> GetAll()
        {
            return await this.db.Subjects.Select(x=> new SubjectModel { Id = x.Id, Name = x.Name }).ToListAsync();
        }
    }
}
