namespace Pishtova.Data.Model
{
    public class UserBadge
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string BadgeId { get; set; }

        public Badge Badge { get; set; }
    }
}
