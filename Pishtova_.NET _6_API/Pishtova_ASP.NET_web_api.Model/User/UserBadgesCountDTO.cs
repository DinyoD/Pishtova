using System.Collections.Generic;

namespace Pishtova_ASP.NET_web_api.Model.User
{
    public class UserBadgesCountDTO
    {
        public UserBadgesCountDTO()
        {
            this.Badges = new HashSet<BadgeCountModel>();
        }
        public ICollection<BadgeCountModel> Badges { get; set; }
    }
}