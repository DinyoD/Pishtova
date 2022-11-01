namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Services.Data;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Subject;

    public class SubjectsController : ApiController
    {
        private readonly ISubjectService subjectService;
        private readonly IScoreService scoreService;

        public SubjectsController(
            ISubjectService subjectService,
            IScoreService scoreService)
        {
            this.subjectService = subjectService;
            this.scoreService = scoreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.subjectService.GetAllAsync();
            if (!result.IsSuccessful) return this.Error(result);
            var modifiedData = result.Data.Select(x => new SubjectBaseModel { Id = x.Id, Name = x.Name }).ToList();
            return Ok(modifiedData);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOneById([FromRoute]int id)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(id)) return this.Error(operationResult);

            var result = await this.subjectService.GetByIdAsync(id);
            if (!result.IsSuccessful) return this.Error(result);
            var modifiedData = new SubjectBaseModel { Id = result.Data.Id, Name = result.Data.Name };
            return Ok(modifiedData);
        }
    }
}
