namespace Pishtova.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class SchoolsSeeder : ISeeder
    {
        public async Task SeedAsync(PishtovaDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Schools.Any())
            {
                return;
            }
            var allSchoolsByTownAndMunicipality = new List<Object>();
            await SeedSchoolAsync(dbContext, allSchoolsByTownAndMunicipality);
        }

        private Task SeedSchoolAsync(PishtovaDbContext dbContext, object allSchoolsByTownAndMunicipality)
        {
            throw new NotImplementedException();
        }
    }
}
