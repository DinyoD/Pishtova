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

        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ICollection<SubjectModel>> All()
        {
            return await this.subjectService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SubjectModel>> getOneById(int id)
        {
            var result = await this.subjectService.GetOneById(id);
            if (result == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect Subject ID!" });
            }
            return result;
        }
    }
}
