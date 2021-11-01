namespace Pishtova_ASP.NET_web_api.Model.School
{
    using Pishtova_ASP.NET_web_api.Model.Town;

    public class SchoolModel
    {
        public string Name { get; set; }

        public int TownId { get; set; }

        public TownModel Town { get; set; }
    }
}
