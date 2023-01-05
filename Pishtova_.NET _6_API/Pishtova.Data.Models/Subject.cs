namespace Pishtova.Data.Model
{
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;

    public class Subject : BaseDeletableModel<string>
    {
        public Subject()
        {
            this.Tests = new HashSet<Test>();
            this.Themes = new HashSet<Theme>();
            this.Categories = new HashSet<SubjectCategory>();
        }

        public string Name { get; set; }

        public virtual ICollection<SubjectCategory> Categories { get; set; }

        public virtual ICollection<Test> Tests { get; set; }

        public virtual ICollection<Theme> Themes { get; set; }
    }
}
