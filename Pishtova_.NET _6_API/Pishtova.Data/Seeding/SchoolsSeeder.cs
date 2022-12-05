namespace Pishtova.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Pishtova.Data.Model;
    using Pishtova.Services;
    using Pishtova.Services.Models;

    internal class SchoolsSeeder : ISeeder
    {
        public async Task SeedAsync(PishtovaDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Schools.Any())
            {
                return;
            }
            var helpers = serviceProvider.GetRequiredService<IHelpers>();
            var schoolString = File.ReadAllText(@"C:\Users\Dinyo\Desktop\Pishtova-docs\schools.txt");
            var allSchoolsByTownAndMunicipality = helpers.ExtractAllSchoolsbyTownsAndMunicipality(schoolString);
            await SeedSchoolAsync(dbContext, allSchoolsByTownAndMunicipality);
        }

        private async Task SeedSchoolAsync(
            PishtovaDbContext dbContext, 
            ICollection<SchoolDTO> allSchoolsByTownAndMunicipality
            )
        {
            foreach (var school in allSchoolsByTownAndMunicipality)
            {
                var municipalityName = school.TownDTO.MunicipalityDTO.Name;
                var municipality = await dbContext.Municipalities.FirstOrDefaultAsync(x => x.Name == municipalityName);

                if (municipality == null)
                {
                    municipality = new Municipality { Name = municipalityName };
                    await dbContext.Municipalities.AddAsync(municipality);
                    await dbContext.SaveChangesAsync();
                }

                var townName = school.TownDTO.Name;
                var town = await dbContext.Towns.FirstOrDefaultAsync(x => x.Name == townName);

                if (town == null)
                {
                    town = new Town { Name = townName, MunicipalityId = municipality.Id };
                    await dbContext.Towns.AddAsync(town);
                    await dbContext.SaveChangesAsync();
                }

                var schoolName = school.Name;
                var newSchool = await dbContext.Schools.FirstOrDefaultAsync(x => x.Name == schoolName && x.TownId == town.Id);

                if (newSchool == null)
                {
                    newSchool = new School 
                    { 
                        Name = schoolName,
                        TownId = town.Id
                    };

                    await dbContext.Schools.AddAsync(newSchool);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
