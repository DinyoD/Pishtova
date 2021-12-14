namespace Pishtova_ASP.NET_web_api.Model.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserModel
    {
        [Required]
        [RegularExpression("^[\\S]+$")]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(4, 12)]
        public int Grade { get; set; }

        public int SchoolId { get; set; }

        public string ClientURI { get; set; }

        [Required]
        [RegularExpression("^[\\S]+$")]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
