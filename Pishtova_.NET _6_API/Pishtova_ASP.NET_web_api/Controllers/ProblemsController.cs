
namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Problem;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.Test;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class ProblemsController: ApiController
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }


        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ICollection<ProblemDTO>> generateTest(int id)
        {
            var testPattern = new Pattern().ProblemsSubjectIDs[id];
            var result = await this.problemService.GenerateTest(testPattern);
            if (result == null)
            {
                return (ICollection<ProblemDTO>)BadRequest(new ErrorResult { Message = "Uncorrect Subject ID!" });
            }
            return result;
        }
    }
}
