using Pishtova.Data.Model;
using Pishtova_ASP.NET_web_api.Model.UserBadge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pishtova.Services.Data
{
    public interface IUsersBadgesService
    {
        Task CreatAsync(UserBadge userBadge);


        //TODO GetAll method with filter
        Task<ICollection<UserBadge>> GetUserAllBadgesAsync(string userId);
    }

}
