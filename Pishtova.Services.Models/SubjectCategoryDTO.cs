namespace Pishtova.Services.Models
{
    using System.Collections.Generic;

    public class SubjectCategoryDTO
    {
        public string Name { get; set; }

        public ICollection<ProblemDTO> Problems { get; set; }
    }
}
