namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System.Collections.Generic;

    public class SubjectCategory : BaseDeletableModel<int> 
    {
        public SubjectCategory()
        {
            this.Problems = new HashSet<Problem>();
        }
        public string Name { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public virtual ICollection<Problem> Problems { get; set; }
    }
}
