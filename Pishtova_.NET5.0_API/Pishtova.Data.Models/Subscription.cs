namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System;

    public class Subscription: BaseDeletableModel<string>
    {

        public Subscription()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string CustomerId { get; set; }

        public string Status { get; set; }

        public DateTime CurrentPeriodEnd { get; set; }
    }
}
