using Pishtova.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pishtova.Services.Data
{
    public interface IUsersBadgesService
    {
        Task CreateUserBadgeAsync(string userId, string badgeId);

        Task<ICollection<UserBadge>> GetUserAllBadgesAsync(string userId);
    }

}
