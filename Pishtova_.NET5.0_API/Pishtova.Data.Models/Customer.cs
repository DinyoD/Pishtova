namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System;

    public class Customer : BaseDeletableModel<string>
    {

        public Customer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
