namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author: BaseDeletableModel<string>
    {
        public Author()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Works = new HashSet<Work>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Index { get; set; }

        public ICollection<Work> Works { get; set; }
    }
}
