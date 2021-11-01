namespace Pishtova_ASP.NET_web_api.Model.Town
{
    using Pishtova_ASP.NET_web_api.Model.Municipality;

    public class TownModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MunicipalityId { get; set; }
    }
}
