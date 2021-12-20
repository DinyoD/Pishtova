namespace Pishtova.Services.Models
{
    using System.Collections.Generic;

    public class SubjectDTO
    {
        public string Name { get; set; }

        public ICollection<SubjectCategoryDTO> Categories { get; set; }
    }
}
