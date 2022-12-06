namespace Pishtova.Data.Model
{
    using System;

    using Pishtova.Data.Common.Model;

    public class Answer : BaseDeletableModel<string>
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public string ProblemId { get; set; }

        public Problem Problem { get; set; }

        public string Row { get; set; }
    }
}
