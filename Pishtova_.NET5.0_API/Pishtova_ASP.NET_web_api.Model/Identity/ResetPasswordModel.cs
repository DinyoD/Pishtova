namespace Pishtova_ASP.NET_web_api.Model.Identity
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
