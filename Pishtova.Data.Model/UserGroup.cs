namespace Pishtova.Data.Model
{
    public class UserGroup
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string GroupId { get; set; }

        public Group Group { get; set; }
    }
}
