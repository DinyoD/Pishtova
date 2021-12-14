namespace Pishtova_ASP.NET_web_api.Model.Results
{
    public interface ILoginResult
    {
        public string Token { get; set; }
    }
    public class LoginResult : ILoginResult
    {
        public string Token { get; set; }
    }
}
