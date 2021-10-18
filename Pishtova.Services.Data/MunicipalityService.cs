namespace Pishtova.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Pishtova.Data;
    using Pishtova.Data.Model;

    public class MunicipalityService : IMunicipalityService
    {
        private readonly PishtovaDbContext db;

        public MunicipalityService(PishtovaDbContext db)
        {
            if (db is null)
            {
                throw new ArgumentNullException(nameof(db));
            };

            this.db = db;
        }

        public async Task<int> CreateAsync(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var municipality = new Municipality { Name = name };
            await this.db.Municipalities.AddAsync(municipality);
            await this.db.SaveChangesAsync();

            return municipality.Id;
        }
    }
}
