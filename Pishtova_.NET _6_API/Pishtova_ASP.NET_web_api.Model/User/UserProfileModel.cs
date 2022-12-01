namespace Pishtova_ASP.NET_web_api.Model.User
{
    using Pishtova_ASP.NET_web_api.Model.School;

    public class UserProfileModel: UserBaseModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PictureUrl { get; set; }

        public SchoolBasicModel School { get; set; }

        public string TownName { get; set; }
    }
}
