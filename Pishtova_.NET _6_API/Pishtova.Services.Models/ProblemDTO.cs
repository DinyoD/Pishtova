namespace Pishtova.Services.Models
{
    using System.Collections.Generic;

    public class ProblemDTO: ProblemBaseDTO
    {
        public ICollection<AnswerDTO> Answers { get; set; }
    }
}
