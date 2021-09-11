﻿namespace Pishtova.Data.Model
{
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;

    public class Subject : BaseDeletableModel<int>
    {
        public Subject()
        {
            this.SubjectScores = new HashSet<Score>();
            this.Categories = new HashSet<SubjectCategory>();
        }

        public string Name { get; set; }

        public virtual ICollection<Score> SubjectScores { get; set; }

        public virtual ICollection<SubjectCategory> Categories { get; set; }
    }
}
