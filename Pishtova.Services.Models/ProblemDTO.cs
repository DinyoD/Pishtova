namespace Pishtova.Services.Models
{
    using System.Collections.Generic;

    public class ProblemDTO
    {
        public string QuestionText { get; set; }
        public ICollection<AnswerDTO> Answers { get; set; }
        public string Hint { get; set; }
        public string PictureUrl { get; set; }
    }
}
