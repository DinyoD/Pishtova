using Pishtova.Data.Common.Model;

namespace Pishtova.Data.Model
{
    public class UserBadge : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string BadgeId { get; set; }

        public Badge Badge { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }
    }
}
