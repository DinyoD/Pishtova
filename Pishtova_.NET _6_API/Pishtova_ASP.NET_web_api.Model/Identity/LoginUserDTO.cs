namespace Pishtova_ASP.NET_web_api.Model.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
