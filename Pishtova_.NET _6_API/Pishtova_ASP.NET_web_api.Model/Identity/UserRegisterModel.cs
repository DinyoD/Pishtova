namespace Pishtova_ASP.NET_web_api.Model.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterModel
    {
        [Required]
        [RegularExpression("^[A-za-z\\p{L}]{1}[A-za-z\\p{L} -]{1,28}[A-za-z\\p{L}]{1}$")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(4, 12)]
        public int Grade { get; set; }

        public int SchoolId { get; set; }

        public string ClientURI { get; set; }

        [Required]
        [RegularExpression("^[\\S]{8,30}$")]
        public string Password { get; set; }
    }
}
