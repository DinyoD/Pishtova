namespace Pishtova.Data.Model
{
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;

    public class Town : BaseDeletableModel<int>
    {
        public Town()
        {
            this.Users = new HashSet<User>();
        }

        public string Name { get; set; }

        public  virtual ICollection<User> Users { get; set; }
    }
}
