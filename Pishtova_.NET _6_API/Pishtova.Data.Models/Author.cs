﻿namespace Pishtova.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Pishtova.Data.Common.Model;

    public class Author: BaseDeletableModel<string>
    {
        public Author()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Poems = new HashSet<Poem>();
        }

        [Required]
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public ICollection<Poem> Poems { get; set; }
    }
}
