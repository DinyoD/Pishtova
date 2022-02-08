namespace Pishtova.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.Municipality;

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

        public async Task<ICollection<MunicipalityModel>> GetAllAsync()
        {
            return await  this.db.Municipalities
                .Where<Municipality>(x => x.IsDeleted == false)
                .Select(x => new MunicipalityModel { Id = x.Id, Name = x.Name })
                .ToListAsync();
        }

        public async Task<MunicipalityModel> GetOneByIdAsync(int id)
        {
            var m =  await this.db.Municipalities
                .FindAsync(id);
            return new MunicipalityModel { Id = m.Id, Name = m.Name };
        }
    }
}
