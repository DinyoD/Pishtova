namespace Pishtova.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.Town;

    public class TownService : ITownService
    {
        private readonly PishtovaDbContext db;

        public TownService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<int> CreateAsync(string name, int municipalityId )
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (municipalityId == 0) throw new ArgumentNullException(nameof(municipalityId));

            var town = new Town {Name = name, MunicipalityId = municipalityId};

            await db.Towns.AddAsync(town);
            await db.SaveChangesAsync();

            return town.Id;
        }

        public async Task<ICollection<TownModel>> GetAllByMunicipalityId(int municipalityId)
        {
            if (municipalityId == 0) throw new ArgumentNullException(nameof(municipalityId));

            return await this.db.Towns
                .Where(x => x.MunicipalityId == municipalityId)
                .Select(x => new TownModel{ 
                    Id = x.Id,
                    Name = x.Name,
                    MunicipalityId = x.MunicipalityId
                })
                .ToListAsync();
        }
    }
}
