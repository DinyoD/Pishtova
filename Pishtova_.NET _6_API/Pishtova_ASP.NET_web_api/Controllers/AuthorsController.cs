namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;

    public class AuthorsController : ApiController
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService ?? throw new System.ArgumentNullException(nameof(authorService));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll ([FromQuery]int subjectId)
        {
            var result = await this.authorService.GetAuthorsWithWorksAsync(subjectId);
            if (!result.IsSuccessful) return this.Error(result);

            var authors = result.Data;
            if (authors == null || authors.Count == 0) return this.NotFound();

            return this.Ok(authors);
        }
    }
}
