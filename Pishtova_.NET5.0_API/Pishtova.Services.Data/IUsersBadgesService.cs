using Pishtova.Data.Model;
using Pishtova_ASP.NET_web_api.Model.UserBadge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pishtova.Services.Data
{
    public interface IUsersBadgesService
    {
        Task CreateUserBadgeAsync(UserBadgeModel model);

        Task<ICollection<UserBadgeWithCodeModel>> GetUserAllBadgesAsync(string userId);
    }

}
