
using System.Collections.Generic;

namespace Pishtova.Services.Models
{
    public class ThemeDTO
    {
        public string Name { get; set; }

        public string SubjectId { get; set; }

        public ICollection<PoemDTO> PoemDTOs { get; set; }
    }
}
