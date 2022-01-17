namespace Pishtova_ASP.NET_web_api.Model.Problem
{
    using Pishtova_ASP.NET_web_api.Model.Answer;
    using System.Collections.Generic;
    public class ProblemModel
    {
        public string Id { get; set; }

        public string PictureUrl { get; set; }

        public int SubjectCategoryId { get; set; }

        public string QuestionText { get; set; }

        public string Hint { get; set; }

        public ICollection<AnswerModel> Answers { get; set; }
    }
}
