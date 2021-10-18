namespace Pishtova.Services.Data
{
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using System;
    using System.Threading.Tasks;
    public class SchoolService : ISchoolService
    {
        private readonly PishtovaDbContext db;

        public SchoolService(PishtovaDbContext db)
        {
            if (db is null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            this.db = db;
        }
        public async Task CreateAsync(string name, int townId)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            };
            if (townId == 0)
            {
                throw new ArgumentNullException(nameof(townId));
            }
            var school = new School
            {
                Name = name,
                TownId = townId,
            };

            await this.db.Schools.AddAsync(school);
            await this.db.SaveChangesAsync();

        }
    }

}
