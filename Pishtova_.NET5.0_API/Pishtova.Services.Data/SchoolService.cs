namespace Pishtova.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.School;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class SchoolService : ISchoolService
    {
        private readonly PishtovaDbContext db;

        public SchoolService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task CreateAsync(string name, int townId)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (townId == 0) throw new ArgumentNullException(nameof(townId));

            var school = new School{ Name = name, TownId = townId};

            await this.db.Schools.AddAsync(school);
            await this.db.SaveChangesAsync();
        }

        public async Task<ICollection<SchoolForRegistrationModel>> GetAllByTownId(int townId)
        {
            return await this.db.Schools
                .Where(x => x.TownId == townId)
                .Select(x => new SchoolForRegistrationModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    TownId = x.TownId
                })
                .ToListAsync();
        }
    }
}
