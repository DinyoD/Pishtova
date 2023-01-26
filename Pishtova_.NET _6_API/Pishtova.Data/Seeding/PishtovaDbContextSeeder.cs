namespace Pishtova.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PishtovaDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(PishtovaDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>
                          {
                              //new SchoolsSeeder(),
                              //new SubjectsSeeder(),
                              //new BadgesSeeder(),
                              //new UsersBadgesSeeder(),
                              //new ThemeSeeder()
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
