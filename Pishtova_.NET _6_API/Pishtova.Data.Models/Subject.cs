namespace Pishtova.Data.Model
{
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;

    public class Subject : BaseDeletableModel<string>
    {
        public Subject()
        {
            this.Categories = new HashSet<SubjectCategory>();
            this.Tests = new HashSet<Test>();
        }

        public string Name { get; set; }

        public virtual ICollection<SubjectCategory> Categories { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
