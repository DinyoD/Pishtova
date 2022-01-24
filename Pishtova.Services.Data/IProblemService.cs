namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.Problem;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProblemService
    {
        Task<ICollection<ProblemModel>> GenerateTest(List<int> testPattern);
    }
}
