using Pishtova.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pishtova.Data.Seeding
{
    public class UsersBadgesSeeder : ISeeder
    {

        private readonly List<UserBadge> entities = new()
        {
            new UserBadge { BadgeId = "46f117e5-66e8-4ea7-980b-90c29aa9678f", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "46f117e5-66e8-4ea7-980b-90c29aa9678f", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 3, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "46f117e5-66e8-4ea7-980b-90c29aa9678f", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "46f117e5-66e8-4ea7-980b-90c29aa9678f", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 3, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "46f117e5-66e8-4ea7-980b-90c29aa9678f", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "46f117e5-66e8-4ea7-980b-90c29aa9678f", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "a9c60536-57e2-411e-ab23-fed1c3809f5a", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "6396d591-7b44-4d74-b4f1-1473e98d6ba8", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "6396d591-7b44-4d74-b4f1-1473e98d6ba8", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "6396d591-7b44-4d74-b4f1-1473e98d6ba8", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 3, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "51f04915-a14a-45bc-8c78-a47826d5e002", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "51f04915-a14a-45bc-8c78-a47826d5e002", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
            new UserBadge { BadgeId = "51f04915-a14a-45bc-8c78-a47826d5e002", UserId = "66676649-6ec5-483c-82ea-36a1c0599e56", Test = new Test { SubjectId = 4, UserId = "66676649-6ec5-483c-82ea-36a1c0599e56" } },
        };
        public async  Task SeedAsync(PishtovaDbContext dbContext, IServiceProvider serviceProvider)
        {
            await dbContext.UsersBadges.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}
