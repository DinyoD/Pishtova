namespace Pishtova_ASP.NET_web_api.Model.Results
{
    public interface IErrorResult
    {
        public string Error { get; set; }
    }
    public class ErrorResult : IErrorResult
    {
        public string Error { get; set; }
    }
}
