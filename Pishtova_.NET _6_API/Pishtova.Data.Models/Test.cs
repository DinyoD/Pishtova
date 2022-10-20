namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System.Collections.Generic;

    public class Test : BaseDeletableModel<int>
    {
        public Test()
        {
            this.Badges = new HashSet<UserBadge>();
        }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int SubjectId { get; set; }

        public int? Score { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ICollection<UserBadge> Badges { get; set; }
    }
}
