namespace Pishtova.Data.Model
{
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;

    public class Town : BaseDeletableModel<int>
    {
        public Town()
        {
            this.Schools = new HashSet<School>();
        }

        public string Name { get; set; }

        public  virtual ICollection<School> Schools { get; set; }
    }
}
