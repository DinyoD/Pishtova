namespace Pishtova_ASP.NET_web_api.Model.User
{
    using System.Collections.Generic;

    public class UserModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public int Grade { get; set; }

        public string SchoolName { get; set; }

        public string TownName { get; set; }

        public UserPointStatsModel Stats { get; set; }

        //public virtual ICollection<UserBadge> BadgeUsers { get; set; }

        //public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
