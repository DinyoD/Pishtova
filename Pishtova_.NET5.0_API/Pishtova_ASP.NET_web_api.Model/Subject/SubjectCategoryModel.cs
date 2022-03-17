namespace Pishtova_ASP.NET_web_api.Model.User
{
    using Pishtova_ASP.NET_web_api.Model.Subject;

    public class SubjectCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SubjectDTO Subject { get; set; }
    }
}
