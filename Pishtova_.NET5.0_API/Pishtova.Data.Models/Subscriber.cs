namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System;

    public class Subscriber: BaseDeletableModel<string>
    {

        public Subscriber()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string CustomerId { get; set; }

        public string Status { get; set; }

        public DateTime CurrentPeriodEnd { get; set; }
    }
}
