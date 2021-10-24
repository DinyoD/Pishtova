namespace Pishtova.Data.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Pishtova.Data.Common.Model;

    public class Score : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string ProblemId { get; set; }

        public Problem Problem { get; set; }

        [Range(0, 1)]
        public int Points { get; set; }
    }
}
