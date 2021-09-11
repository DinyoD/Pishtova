namespace Pishtova.Data.Model
{
    using System;
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;

    public class Group : BaseDeletableModel<string>
    {
        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
            this.GroupUsers = new HashSet<UserGroup>();
        }

        public string Name { get; set; }

        public virtual ICollection<UserGroup> GroupUsers { get; set; }
    }
}
