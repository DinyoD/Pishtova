namespace Pishtova_ASP.NET_web_api.Model.Results
{
    public interface IErrorResult
    {
        public string Message { get; set; }
    }
    public class ErrorResult : IErrorResult
    {
        public string Message { get; set; }
    }
}
