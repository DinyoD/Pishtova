namespace Pishtova.Data.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Pishtova.Data.Common.Model;

    public class Score : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        [Range(0, 20)]
        public int Points { get; set; }
    }
}
