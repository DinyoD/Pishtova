namespace Pishtova_ASP.NET_web_api.Model.User
{
    using Pishtova_ASP.NET_web_api.Model.School;
    using System.Collections.Generic;

    public class UserProfileDTO: UserBaseModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PictureUrl { get; set; }

        public SchoolForUserModel School { get; set; }

        public string TownName { get; set; }

        public UserPointStatsModel Stats { get; set; }

        //public virtual ICollection<UserBadge> BadgeUsers { get; set; }

        //public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
