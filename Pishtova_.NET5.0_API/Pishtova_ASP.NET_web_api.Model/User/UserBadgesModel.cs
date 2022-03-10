using System.Collections.Generic;

namespace Pishtova_ASP.NET_web_api.Model.User
{
    public class UserBadgesModel
    {
        public UserBadgesModel()
        {
            this.Badges = new HashSet<BadgeModel>();
        }
        public ICollection<BadgeModel> Badges { get; set; }
    }
}