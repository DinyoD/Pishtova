namespace Pishtova.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Pishtova.Data.Common.Model;

    public class Theme : BaseDeletableModel<string>
    {
        public Theme()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Poems = new HashSet<Poem>();
        }

        [Required]
        public string Name { get; set; }

        public string SubjectId { get; set; }

        public Subject Subject { get; set; }

        public virtual ICollection<Poem> Poems { get; set; }
    }
}
