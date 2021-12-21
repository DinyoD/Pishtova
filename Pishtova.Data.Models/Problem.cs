namespace Pishtova.Data.Model
{
    using System;
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;

    public class Problem : BaseDeletableModel<string>
    {
        public Problem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Answers = new HashSet<Answer>();
        }

        public string PictureUrl { get; set; }

        public int SubjectCategoryId { get; set; }

        public SubjectCategory SubjectCategory { get; set; }

        public string QuestionText { get; set; }

        public string Hint { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
