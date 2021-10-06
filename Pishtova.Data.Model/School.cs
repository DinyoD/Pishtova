namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System.Collections.Generic;

    public class School : BaseDeletableModel<int>
    {
        public School()
        {
            this.Users = new HashSet<User>();
        }

        public string Name { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
