namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SubjectController : ApiController
    {
        private readonly ISubjectService subjectService;
        private readonly IScoreService scoreService;

        public SubjectController(
            ISubjectService subjectService,
            IScoreService scoreService)
        {
            this.subjectService = subjectService;
            this.scoreService = scoreService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ICollection<SubjectModel>> All()
        {
            return await this.subjectService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SubjectModel>> GetOneById(int id)
        {
            var result = await this.subjectService.GetOneById(id);
            if (result == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect Subject ID!" });
            }
            return result;
        }

        [HttpGet]
        [Route("{id}/ranking")]
        public async Task<ActionResult<SubjectRankingByScores>> Ranking(int id)
        {
            return await this.scoreService.GetUsersScoreBySubjectIdAsync(id);
            
        }
    }
}
