
namespace Pishtova_ASP.NET_web_api.Model.Payment
{
    using System.Collections.Generic;

    public class StripeProductModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<StripePriceModel> Prices { get; set; }
    }
}
