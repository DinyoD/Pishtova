namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Author;
    using Pishtova_ASP.NET_web_api.Model.Work;

    public class AuthorsController : ApiController
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService ?? throw new System.ArgumentNullException(nameof(authorService));
        }


        [HttpGet]
        [Route("subject/{subjectId}")]
        public async Task<IActionResult> GetAll ([FromRoute]string subjectId)
        {
            var result = await this.authorService.GetAuthorsWithWorksBySubjectIdAsync(subjectId);
            if (!result.IsSuccessful) return this.Error(result);

            var authors = result.Data;
            if (authors == null || authors.Count == 0) return this.NotFound();

            var authorModels = authors.Select(this.ToAuthorModel).ToList();

            return this.Ok(authorModels);
        }

        private AuthorModel ToAuthorModel(Author author)
        {
            return new AuthorModel
            {
                Name = author.Name,
                Index = author.Index,
                Works = author.Works.Select(w => new WorkModel
                                    {
                                        Name = w.Name,
                                        Index = w.Index
                                    })
                                    .ToList()
            };
        }
    }
}
