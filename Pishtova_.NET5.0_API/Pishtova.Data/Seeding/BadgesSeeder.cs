namespace Pishtova.Data.Seeding
{
    using Pishtova.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BadgesSeeder : ISeeder
    { 
        private static Dictionary<int,string> BadgesCodeAndName => new()
        {
            { 1070, "70%CorrectAnswerBadge" },
            { 1080, "80%CorrectAnswerBadge" },
            { 1090, "90%CorrectAnswerBadge" },
            { 1100, "100%CorrectAnswerBadge" },
            { 2010, "10TestsComplitedBadge" },
            { 2020, "20TestsComplitedBadge" },
            { 2050, "50TestsComplitedBadge" },
            { 2100, "100TestsComplitedBadge" },
        };
    
        public async Task SeedAsync(PishtovaDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var kvp in BadgesCodeAndName)
            {
                if (!dbContext.Badges.Any(x => x.Code == kvp.Key))
                {
                    await dbContext.Badges.AddAsync(new Badge
                    {
                        Code = kvp.Key,
                        Name = kvp.Value
                    });
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}