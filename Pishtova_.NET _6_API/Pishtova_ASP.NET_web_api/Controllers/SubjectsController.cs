namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using Pishtova.Data.Model;

    public class SubjectsController : ApiController
    {
        private readonly ISubjectService subjectService;

        public SubjectsController(
            ISubjectService subjectService
            )
        {
            this.subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.subjectService.GetAllAsync();
            if (!result.IsSuccessful) return this.Error(result);
            var subjects = result.Data;
            var subjectViewModels = subjects.Select(this.ToSubjectBaseModel).ToList();
            return Ok(subjectViewModels);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOneById([FromRoute]string id)
        {
            var result = await this.subjectService.GetByIdAsync(id);
            if (!result.IsSuccessful) return this.Error(result);
            var subject = result.Data;
            if (subject == null) return this.NotFound();
            return Ok(this.ToSubjectBaseModel(subject));
        }

        private SubjectBaseModel ToSubjectBaseModel(Subject subject)
        {
            return new SubjectBaseModel { Id = subject.Id, Name = subject.Name };
        }
    }
}
