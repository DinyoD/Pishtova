namespace Pishtova_ASP.NET_web_api.Model.User
{
    public class UserInfoDTO: UserBaseModel
    {
        public string SchoolName { get; set; }

        public string TownName { get; set; }

        public string PictureUrl { get; set; }
    }
}
