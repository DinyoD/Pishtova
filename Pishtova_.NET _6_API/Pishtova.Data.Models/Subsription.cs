namespace Pishtova.Data.Model
{
    using System;
    using Pishtova.Data.Common.Model;

    public class Subsription: BaseDeletableModel<string>
    {

        public Subsription()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string CustomerId { get; set; }

        public string Status { get; set; }

        public DateTime CurrentPeriodEnd { get; set; }
    }
}
