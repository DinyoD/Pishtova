using System;
using System.Collections.Generic;
namespace Pishtova.Data.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Pishtova.Data.Common.Model;

    public class Poem : BaseDeletableModel<string>
    {
        public Poem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }

        public string AuthorId { get; set; }

        public Author Author { get; set; }

        public string ThemeId { get; set; }

        public Theme Theme { get; set; }

        public string TextUrl { get; set; }

        public string TextLink { get; set; }

        public string AnalysisUrl { get; set; }

        public string ExtrasUrl { get; set; }
    }
}
