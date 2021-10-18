namespace Pishtova.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Pishtova.Data;
    using Pishtova.Data.Model;

    public class TownService : ITownService
    {
        private readonly PishtovaDbContext db;

        public TownService(PishtovaDbContext db)
        {
            if (db is null)
            {
                throw new ArgumentNullException(nameof(db));
            };

            this.db = db;
        }

        public async Task<int> CreateAsync(string name, int municipalityId)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (municipalityId == 0)
            {
                throw new ArgumentNullException(nameof(municipalityId));
            }

            var town = new Town 
            {
                Name = name,
                MunicipalityId = municipalityId,
            };

            await db.Towns.AddAsync(town);
            await db.SaveChangesAsync();

            return town.Id;
        }
    }
}
