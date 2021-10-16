namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System.Collections.Generic;

    public class Municipality : BaseDeletableModel<int>
    {
        public Municipality()
        {
            this.Towns = new HashSet<Town>();
        }

        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; }
    }
}
