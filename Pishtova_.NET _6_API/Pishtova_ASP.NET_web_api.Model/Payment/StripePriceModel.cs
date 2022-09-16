namespace Pishtova_ASP.NET_web_api.Model.Payment
{
    public class StripePriceModel
    {
        public string Id { get; set; }

        public string Subscription { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
